using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DLHBuilder.Terminal
{
    public class TerminalWindow : Form
    {
        public TerminalWindow(Project project = null)
        {
            Text = "DLH Builder - Terminal";
            Width = 1200;
            Height = 700;
            BackColor = Color.Black;
            Controls.Add(Entry = new TerminalEntry(project));
        }

        TerminalEntry Entry { get; set; }
    }
}
