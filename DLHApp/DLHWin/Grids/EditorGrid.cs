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
            ColumnHeaderMouseClick += ColumnSelected;
            MouseClick += DisplayMenu;
        }

        void ColumnSelected(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Selected = false;
                }

                row.Cells[e.ColumnIndex].Selected = true;
            }
        }

        protected virtual void SetColumns()
        {
            Columns.Clear();

            foreach (EditorGridColumn column in GridColumns)
            {
                DataGridViewColumn col = column.GetBaseColumn();
                col.Name = column.Name;
                col.HeaderText = column.Name;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                    ((DataGridViewComboBoxCell)gridCell).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
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

        void DisplayMenu(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Right)
            {
                return;
            }

            if(SelectedCells.Count == 0)
            {
                return;
            }

            GridMenu().Show(this, new Point(e.X, e.Y));
        }

        protected virtual ContextMenuStrip GridMenu()
        {
            ContextMenuStrip output = new ContextMenuStrip();

            ToolStripButton copyBtn = new ToolStripButton() { Text = "Copy" };
            copyBtn.Click += CopyValues;
            output.Items.Add(copyBtn);

            return output;
        }

        protected virtual void CopyValues(object sender, EventArgs e)
        {
            if(SelectedCells.Count == 1)
            {
                Clipboard.SetText(SelectedCells[0].Value.ToString());
                return;
            }

            if(SelectedRows.Count == Rows.Count)
            {
                CopyGridFull();
            }
        }

        protected virtual void CopyGridFull()
        {
            string output = string.Empty;

            for(int i = 0; i < Columns.Count; i++)
            {
                output += (i == 0 ? "" : "\t") + Columns[i].Name;
            }

            foreach(DataGridViewRow row in Rows)
            {
                output += "\n";

                for(int i = 0; i < row.Cells.Count; i++)
                {
                    output += (i == 0 ? "" : "\t") + row.Cells[i].Value.ToString();
                }
            }

            Clipboard.SetText(output);
        }
    }
}
