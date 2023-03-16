using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal class EditorGridCheckCell : EditorGridCell
    {
        public override Type ColumnType => typeof(DataGridViewCheckBoxColumn);

        public override Type CellType => typeof(DataGridViewCheckBoxCell);
    }
}
