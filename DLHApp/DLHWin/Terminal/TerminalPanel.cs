using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Terminal
{
    internal class TerminalPanel : Panel
    {
        public TerminalPanel()
        {
            Dock = DockStyle.Bottom;
            Controls.Add(TerminalBox);
        }

        RichTextBox TerminalBox = new RichTextBox() { Dock = DockStyle.Fill, BackColor = Color.Black, ForeColor = Color.White, Height = 1000, Font = new Font("Cascadia Code", 11) };
    }
}
