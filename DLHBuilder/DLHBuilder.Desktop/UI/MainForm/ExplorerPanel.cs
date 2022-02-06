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
            
            dockitem.Dock = DockStyle.Fill;
            Controls.Add(dockitem);
        }

        public void Reset(Control dockitem)
        {
            Controls.Clear();
            dockitem.Dock = DockStyle.Fill;
            Controls.Add(dockitem);
        }
    }
}
