using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snotepad
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.GetCommandLineArgs().Length < 2)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Run(new FrmMain(Environment.GetCommandLineArgs()[1]));
            }
        }
    }
}
