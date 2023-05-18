using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace snotepad
{
    public partial class FrmPasswd : Form
    {
        public FrmPasswd()
        {
            InitializeComponent();
        }

        public byte[] PasswordValue { get; set; }
        public byte[] IvValue { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPasswd.Text))
            {
                using (SHA256 sha = new SHA256CryptoServiceProvider())
                {
                    PasswordValue = sha.ComputeHash(Encoding.UTF8.GetBytes(txtPasswd.Text));
                }
                using (MD5 md = new MD5CryptoServiceProvider())
                {
                    IvValue = md.ComputeHash(Encoding.UTF8.GetBytes(txtPasswd.Text));
                }
                Close();
            }
        }
    }
}
