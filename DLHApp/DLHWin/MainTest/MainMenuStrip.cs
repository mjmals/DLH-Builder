using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DLHWin.MainTest
{
    internal class MainMenuStrip : MenuStrip
    {
        public MainMenuStrip()
        {
            BackColor = ColorTranslator.FromHtml("#3E3E3E");
            ForeColor = ColorTranslator.FromHtml("#a9a9a9");
            Height = 30;
            Items.Add(FileMenu);
            Items.Add(EditMenu);
            Items.Add(ViewMenu);
            Items.Add(TerminalMenu);
        }

        static Font menuItemFont = new Font("Consolas", 11);

        ToolStripMenuItem FileMenu = new ToolStripMenuItem() { Text = "File", Font = menuItemFont };

        ToolStripMenuItem EditMenu = new ToolStripMenuItem() { Text = "Edit", Font = menuItemFont };

        ToolStripMenuItem ViewMenu = new ToolStripMenuItem() { Text = "View", Font = menuItemFont };

        ToolStripMenuItem TerminalMenu = new ToolStripMenuItem() { Text = "Terminal", Font = menuItemFont };
    }
}
