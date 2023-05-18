using System.Windows.Forms;

namespace snotepad
{
    internal static class Alert
    {
        private static string appname = "Secure Notepad";

        internal static void Info(string s)
        {
            MessageBox.Show(s, appname, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static void Warning(string s)
        {
            MessageBox.Show(s, appname, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal static void Error(string s)
        {
            MessageBox.Show(s, appname, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult Question(string s)
        {
            return MessageBox.Show(s, appname, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
