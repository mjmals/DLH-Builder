using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHWin.MainTest
{
    internal class MainForm : Form
    {
        public MainForm()
        {
            Controls.Add(ActionPanel = new ActionPanel());
            Controls.Add(SidePanel = new SidePanel());
            Controls.Add(Menu = new MainMenuStrip());
            Controls.Add(StatusBar = new StatusBar());
            WindowState = FormWindowState.Maximized;
            Height = 600;
            Width = 800;

            BackColor = ColorTranslator.FromHtml("#1e1e1e");
        }

        MainMenuStrip Menu { get; }

        SidePanel SidePanel { get; }

        ActionPanel ActionPanel { get; }

        StatusBar StatusBar { get; }
    }
}
