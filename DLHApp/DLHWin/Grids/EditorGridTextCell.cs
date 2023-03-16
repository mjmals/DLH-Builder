using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal class EditorGridTextCell : EditorGridCell
    {
        public override Type ColumnType => typeof(DataGridViewTextBoxColumn);

        public override Type CellType => typeof(DataGridViewTextBoxCell);
    }
}
