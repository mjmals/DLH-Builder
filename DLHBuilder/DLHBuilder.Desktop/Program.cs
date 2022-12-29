using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using DLHBuilder.Desktop.UI;

namespace DLHBuilder.Desktop
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;

            // load standard dlls
            foreach (string dll in Directory.GetFiles(appDir, "*.dll"))
            {
                Assembly extension = Assembly.LoadFile(dll);
            }

            // load extension dlls
            string extensionDir = Path.Combine(appDir, "Extensions");

            foreach (string dll in Directory.GetFiles(extensionDir, "*.dll"))
            {
                Assembly extension = Assembly.LoadFile(dll);
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

    }
}
