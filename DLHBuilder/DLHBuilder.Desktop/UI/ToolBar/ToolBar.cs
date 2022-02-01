using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ToolBar : ToolStrip
    {
        public ToolBar()
        {
            ImageList = Images.ImageList;
            Items.Add(OpenButton);
            Items.Add(SaveButton);
        }

        public ToolStripButton OpenButton = new ToolStripButton() { ImageKey = "Open", ToolTipText = "Open Project" };

        public ToolStripButton SaveButton = new ToolStripButton() { ImageKey = "Save", ToolTipText = "Save Project" };
    }
}
