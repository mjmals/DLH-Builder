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
            CellEndEdit += CellUpdated;
            AllowUserToAddRows = false;
        }

        protected virtual void SetColumns()
        {
            Columns.Clear();

            foreach (EditorGridColumn column in GridColumns)
            {
                DataGridViewColumn col = column.GetBaseColumn();
                col.Name = column.Name;
                col.HeaderText = column.Name;
                Columns.Add(col);
            }
        }

        public virtual void Reload()
        {
            ProcessRows();
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
            SetColumns();

            object[] values = ((IEnumerable<object>)RowValues).Cast<object>().ToArray();

            foreach(object value in values)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Tag = value;
                AddCells(row, value);
                Rows.Add(row);
            }
        }

        protected void AddCells(DataGridViewRow row, object value)
        {
            EditorGridColumnCollection gridColumns = GridColumns;

            foreach (EditorGridColumn column in gridColumns)
            {
                PropertyInfo propInfo = value.GetType().GetProperty(column.BaseProperty);
                EditorGridCell cell = (EditorGridCell)Activator.CreateInstance(column.CellType);
                cell.BaseProperty = column.BaseProperty;
                cell.SetValue(propInfo.GetValue(value));

                DataGridViewCell gridCell = (DataGridViewCell)Activator.CreateInstance(cell.CellType);

                if (column is EditorGridDropdownColumn && gridCell is DataGridViewComboBoxCell)
                {
                    ((DataGridViewComboBoxCell)gridCell).DataSource = ((EditorGridDropdownColumn)column).DropdownValues;

                }

                gridCell.Value = cell.Value;
                gridCell.Tag = cell;

                row.Cells.Add(gridCell);
            }

        }

        protected virtual void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            EditorGridCell cell = (EditorGridCell)Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
            cell.SetValue(Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            cell.UpdateBaseProperty(Rows[e.RowIndex].Tag);
        }

        public virtual void AddRow(object value)
        {
            DataGridViewRow row = new DataGridViewRow() { Tag = value };
            AddCells(row, value);
            Rows.Add(row);
        }
    }
}
