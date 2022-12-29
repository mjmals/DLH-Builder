using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components;

namespace DLHBuilder.Desktop.UI
{
    class ToolBar : ToolStrip
    {
        public ToolBar()
        {
            ImageList = Images.Items;
            Items.Add(NewButton);
            Items.Add(OpenButton);
            Items.Add(SaveButton);
        }

        public ToolStripButton NewButton = new ToolStripButton() { ImageKey = "Project", ToolTipText = "New Project" };

        public ToolStripButton OpenButton = new ToolStripButton() { ImageKey = "Folder Open", ToolTipText = "Open Project" };

        public ToolStripButton SaveButton = new ToolStripButton() { ImageKey = "Save", ToolTipText = "Save Project" };
    }
}
