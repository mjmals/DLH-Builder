using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ExplorerPanel : Panel
    {
        public ExplorerPanel(Control dockitem)
        {
            Dock = DockStyle.Left;
            Width = 400;

            Controls.Add(PanelSplitter);

            dockitem.Dock = DockStyle.Fill;
            Controls.Add(dockitem);
        }

        Splitter PanelSplitter = new Splitter() { Dock = DockStyle.Right, Width = 5 };

        public void Reset(Control dockitem)
        {
            Controls.Clear();
            Controls.Add(PanelSplitter);
            dockitem.Dock = DockStyle.Fill;
            Controls.Add(dockitem);
        }
    }
}
