using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Grids
{
    internal class EditorGridDropdownColumn : EditorGridColumn
    {
        public EditorGridDropdownColumn(string name, string baseProperty, Type cellType, string[] dropdownValues) : base(name, baseProperty, cellType)
        {
            DropdownValues = dropdownValues;
        }

        public string[] DropdownValues { get; set; }

        internal override DataGridViewColumn GetBaseColumn()
        {
            DataGridViewComboBoxColumn output = (DataGridViewComboBoxColumn)base.GetBaseColumn();
            output.DataSource = DropdownValues;
            return output;
        }
    }
}
