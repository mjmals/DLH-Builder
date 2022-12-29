using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public abstract class EditorGridPanel : Panel
    {
        public EditorGridPanel(IEditorGridCell cell)
        {
            Cell = cell;
            SetControls();
        }

        protected IEditorGridCell Cell { get; set; }

        public virtual Control[] PanelControls { get; }

        public virtual void SetControls()
        {
            foreach(Control control in PanelControls)
            {
                Controls.Add(control);
            }
        }
    }
}
