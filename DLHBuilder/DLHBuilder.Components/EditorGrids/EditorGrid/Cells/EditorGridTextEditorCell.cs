using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridTextEditorCell : EditorGridObjectCell, IEditorGridCell
    {
        public override EditorGridPanel Panel => new EditorGridTextEditorPanel(this);
    }
}
