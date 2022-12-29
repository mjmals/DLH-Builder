using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridStructure : DataGridView
    {
        public EditorGridStructure(EditorGridColumnDefinition[] columnDefinitions, object[] baseObjects)
        {
            ColumnDefinitions = columnDefinitions;
            BaseObjects = baseObjects;

            Dock = DockStyle.Fill;
            CellBeginEdit += CellEditStart;
            CellEndEdit += CellUpdated;
            CellEnter += CellSelected;
            AllowUserToAddRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            Build();
            ProcessRows();
        }

        EditorGridColumnDefinition[] ColumnDefinitions { get; }

        object[] BaseObjects { get; }

        protected virtual void Build()
        {
            foreach (EditorGridColumnDefinition definition in ColumnDefinitions)
            {
                IEditorGridCell cell = (IEditorGridCell)Activator.CreateInstance(definition.CellType);
                DataGridViewColumn column = (DataGridViewColumn)Activator.CreateInstance(cell.ColumnType);
                column.HeaderText = definition.DisplayName;

                if (column is DataGridViewComboBoxColumn && definition.Datasource != null)
                {
                    DataGridViewComboBoxColumn lookupCol = (DataGridViewComboBoxColumn)column;

                    foreach (var item in definition.Datasource.Values())
                    {
                        if (item.Value is string)
                        {
                            lookupCol.Items.Add(item.Value);
                        }
                        else
                        {
                            lookupCol.DisplayMember = definition.DisplayName;
                            lookupCol.Items.Add(item.Value);
                        }
                    }
                }

                Columns.Add(column);
            }
        }

        public virtual void ProcessRows()
        {
            Rows.Clear();

            foreach (object baseObject in BaseObjects)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Tag = baseObject;

                foreach (EditorGridColumnDefinition definition in ColumnDefinitions)
                {
                    object propertyValue = baseObject.GetType().GetProperty(definition.PropertyName).GetValue(baseObject);

                    IEditorGridCell cell = (IEditorGridCell)Activator.CreateInstance(definition.CellType);
                    cell.SetProperties(baseObject, definition.PropertyName, propertyValue, definition.Datasource);
                    row.Cells.Add((DataGridViewCell)cell);
                }

                Rows.Add(row);
            }
        }

        public virtual void CellEditStart(object sender, DataGridViewCellCancelEventArgs e)
        {
            IEditorGridCell cell = (IEditorGridCell)Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.AllowEdit == false)
            {
                e.Cancel = true;
            }
        }

        public virtual void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            object baseObject = Rows[e.RowIndex].Tag;
            IEditorGridCell cell = (IEditorGridCell)Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ProcessCellUpdate();
        }

        void CellSelected(object sender, DataGridViewCellEventArgs e)
        {
            IEditorGridCell cell = (IEditorGridCell)Rows[e.RowIndex].Cells[e.ColumnIndex];

            // if single cell is selected then handle only this cell
            if(SelectedCells.Count == 1)
            {
                SingleCellSelection(cell);
                return;
            }

            // if multiple cells are selected and they span only one column then handle this as a batch
            if(SelectedCells.Count > 1)
            {
                // analyse the selected cells and check how many columns they span
                DataGridViewCell[] selectedCells = SelectedCells.Cast<DataGridViewCell>().ToArray();
                int[] columnIndexes = selectedCells.Cast<DataGridViewCell>().Select(x => x.ColumnIndex).ToArray();
                int distinctColumnCount = columnIndexes.ToList().Distinct<int>().Count();

                // if the selected cells only span one column then handle these cells
                if(distinctColumnCount == 1)
                {
                    MultipleCellSelection();
                }
            }
        }

        public EventHandler<EditorGridControlChangeEventArgs> CellControlChange { get; set; }

        void OnCellControlChange(IEditorGridCell cell, EditorGridPanel panel)
        {
            CellControlChange?.Invoke(cell, new EditorGridControlChangeEventArgs(panel));
        }

        void SingleCellSelection(IEditorGridCell cell)
        {
            if (cell is EditorGridObjectCell)
            {
                EditorGridObjectCell objCell = (EditorGridObjectCell)cell;
                OnCellControlChange(cell, objCell.Panel);
                return;
            }

            OnCellControlChange(cell, null);
        }

        void MultipleCellSelection()
        {
            IEditorGridCell[] cells = (IEditorGridCell[])SelectedCells.Cast<IEditorGridCell>().ToArray();

            if(cells[0] is EditorGridObjectCell)
            {
                return;
            }

            OnCellControlChange(cells[0], new EditorGridBatchUpdatePanel(cells, cells[0]));
        }
    }
}
