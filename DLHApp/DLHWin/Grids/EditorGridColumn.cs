using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHWin.Grids
{
    internal class EditorGridColumn
    {
        public EditorGridColumn(string name, string baseProperty, Type cellType)
        {
            Name = name;
            BaseProperty = baseProperty;
            CellType = cellType;
        }

        public string Name { get; set; }

        public string BaseProperty { get; set; }

        public Type CellType { get; set; }

        internal virtual DataGridViewColumn GetBaseColumn()
        {
            EditorGridCell cell = (EditorGridCell)Activator.CreateInstance(CellType);
            DataGridViewColumn output = (DataGridViewColumn)Activator.CreateInstance(cell.ColumnType);
            output.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            return output;
        }
    }
}
