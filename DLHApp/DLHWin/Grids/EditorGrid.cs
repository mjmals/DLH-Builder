using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHWin.Grids
{
    internal abstract class EditorGrid : DataGridView
    {
        public EditorGrid()
        {
            Dock = DockStyle.Fill;

            foreach(EditorGridColumn column in GridColumns)
            {
                DataGridViewColumn col = column.GetBaseColumn();
                col.Name = column.Name;
                col.HeaderText = column.Name;
                Columns.Add(col);
            }
        }

        protected virtual object RowValues
        {
            get => _rows;
            set
            {
                _rows = value;
                ProcessRows();
            }
        }

        object _rows { get; set; }

        protected virtual EditorGridColumnCollection GridColumns { get; set; }

        void ProcessRows()
        {
            Rows.Clear();

            object[] values = ((IEnumerable<object>)RowValues).Cast<object>().ToArray();

            foreach(object value in values)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Tag = value;

                foreach(EditorGridColumn column in GridColumns)
                {
                    PropertyInfo propInfo = value.GetType().GetProperty(column.BaseProperty);
                    EditorGridCell cell = (EditorGridCell)Activator.CreateInstance(column.CellType);
                    cell.BaseProperty = column.BaseProperty;
                    cell.SetValue(propInfo.GetValue(value));

                    DataGridViewCell gridCell = (DataGridViewCell)Activator.CreateInstance(cell.CellType);
                    gridCell.Value = cell.Value;
                    gridCell.Tag = cell;
                    
                    row.Cells.Add(gridCell);
                }

                Rows.Add(row);
            }
        }
    }
}
