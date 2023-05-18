using System.Drawing;
using System.Windows.Forms;
using snotepad.Properties;

namespace snotepad
{
    internal static class Core
    {
        public static string LastTitle { get; set; }

        internal static void ChangeTitle(string s, Form f)
        {
            f.Text = s + " - Secure Notepad";
            LastTitle = s;
        }

        internal static void TitleNotSaved(Form f)
        {
            f.Text = LastTitle + " *" + " - Secure Notepad";
        }

        internal static void TitleSaved(Form f)
        {
            f.Text = LastTitle + " - Secure Notepad";
        }

        internal static Font GetFont()
        {
            return Settings.Default.TextFont;
        }

        internal static void SetFont(Font f)
        {
            Settings.Default.TextFont = f;
        }

        internal static void SaveSettings()
        {
            Settings.Default.Save();
        }
    }
}
