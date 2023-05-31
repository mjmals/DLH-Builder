using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal class EditorGridDropdownCell : EditorGridCell
    {
        public override Type ColumnType => typeof(DataGridViewComboBoxColumn);

        public override Type CellType => typeof(DataGridViewComboBoxCell);
    }
}
