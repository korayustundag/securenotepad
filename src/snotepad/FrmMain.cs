using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace snotepad
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(string arg)
        {
            InitializeComponent();
            using (FrmPasswd fp = new FrmPasswd())
            {
                fp.ShowDialog();
                if (fp.PasswordValue != null)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(arg, FileMode.Open, FileAccess.Read))
                        {
                            using (Aes aes = new AesCryptoServiceProvider())
                            {
                                aes.Key = fp.PasswordValue;
                                aes.IV = fp.IvValue;
                                aes.Mode = CipherMode.CBC;
                                aes.Padding = PaddingMode.PKCS7;

                                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                                using (CryptoStream cs = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                                {
                                    using (StreamReader sr = new StreamReader(cs))
                                    {
                                        isTitleChanged = true;
                                        richTextArea.Text = sr.ReadToEnd();
                                        Core.ChangeTitle(Path.GetFileName(arg), this);
                                        isDataChanged = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.Error("Error: " + ex.Message);
                    }
                }
                else
                {
                    Alert.Info("The process was canceled by the user!");
                }
            }
        }

        bool isDataChanged = false;
        bool isTitleChanged = false;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isDataChanged)
            {
                DialogResult dr = Alert.Question("Changes are not saved! Do you want to save the changes and create a new document?");
                if (dr == DialogResult.Yes)
                {
                    Save();
                    richTextArea.Text = "";
                    isDataChanged = false;
                    this.Text = "Secure Notepad";
                }
                else if (dr == DialogResult.No)
                {
                    richTextArea.Text = "";
                    isDataChanged = false;
                    this.Text = "Secure Notepad";
                }
            }
        }

        private void Save()
        {
            if (!string.IsNullOrWhiteSpace(richTextArea.Text))
            {
                using (FrmPasswd fp = new FrmPasswd())
                {
                    fp.ShowDialog();
                    if (fp.PasswordValue != null)
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.Filter = "Secure Text Document|*.stxt";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                bool isSave = false;
                                using (Aes aes = new AesCryptoServiceProvider())
                                {
                                    aes.Key = fp.PasswordValue;
                                    aes.IV = fp.IvValue;
                                    aes.Mode = CipherMode.CBC;
                                    aes.Padding = PaddingMode.PKCS7;
                                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        using (CryptoStream cs = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
                                        {
                                            using (StreamWriter sw = new StreamWriter(cs))
                                            {
                                                sw.Write(richTextArea.Text);
                                                isSave = true;
                                                isDataChanged = false;
                                            }
                                        }
                                    }
                                }
                                if (isSave)
                                {
                                    Core.ChangeTitle(Path.GetFileName(sfd.FileName), this);
                                    Core.TitleSaved(this);
                                    Alert.Info("File saved!");
                                }
                                else
                                {
                                    Core.ChangeTitle(Path.GetFileName(sfd.FileName), this);
                                    Core.TitleSaved(this);
                                    Alert.Error("Not Saved!");
                                }
                            }
                        }
                    }
                    fp.Dispose();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Secure Text Document|*.stxt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (FrmPasswd fp = new FrmPasswd())
                    {
                        fp.ShowDialog();
                        if (fp.PasswordValue != null)
                        {
                            try
                            {
                                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                                {
                                    using (Aes aes = new AesCryptoServiceProvider())
                                    {
                                        aes.Key = fp.PasswordValue;
                                        aes.IV = fp.IvValue;
                                        aes.Mode = CipherMode.CBC;
                                        aes.Padding = PaddingMode.PKCS7;

                                        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                                        using (CryptoStream cs = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                                        {
                                            using (StreamReader sr = new StreamReader(cs))
                                            {
                                                isTitleChanged = true;
                                                richTextArea.Text = sr.ReadToEnd();
                                                Core.ChangeTitle(Path.GetFileName(ofd.FileName), this);
                                                isDataChanged = false;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Alert.Error("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Alert.Info("The process was canceled by the user!");
                        }
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void richTextArea_TextChanged(object sender, EventArgs e)
        {
            if (isTitleChanged)
            {
                isTitleChanged = false;
            }
            else
            {
                if (!isDataChanged)
                {
                    Core.TitleNotSaved(this);
                    isDataChanged = true;
                }
            }
            toolStripLblLength.Text = "Length:" + richTextArea.Text.Length.ToString();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDataChanged)
            {
                DialogResult dr = Alert.Question("Changes are not saved! Do you want to save the changes and close Secure Notepad?");
                if (dr == DialogResult.Yes)
                {
                    Save();
                    e.Cancel = false;
                }
                else if (dr == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextArea.CanUndo)
            {
                richTextArea.Undo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextArea.CanRedo)
            {
                richTextArea.Redo();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextArea.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextArea.Paste();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = Core.GetFont();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    richTextArea.Font = fd.Font;
                    Core.SetFont(fd.Font);
                    Core.SaveSettings();
                }
            }
        }

        private void contextUndo_Click(object sender, EventArgs e)
        {
            if (richTextArea.CanUndo)
            {
                richTextArea.Undo();
            }
        }

        private void contextRedo_Click(object sender, EventArgs e)
        {
            if (richTextArea.CanRedo)
            {
                richTextArea.Redo();
            }
        }

        private void contextCut_Click(object sender, EventArgs e)
        {
            richTextArea.Cut();
        }

        private void contextCopy_Click(object sender, EventArgs e)
        {
            richTextArea.Copy();
        }

        private void contextPaste_Click(object sender, EventArgs e)
        {
            richTextArea.Paste();
        }

        private void contextSelectAll_Click(object sender, EventArgs e)
        {
            richTextArea.SelectAll();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAbout fa = new FrmAbout())
            {
                fa.ShowDialog();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
