using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGrid : EditorGrid
    {
        public DataStructEditorGrid(DataStruct dataStruct)
        {
            DataStruct = dataStruct;
            RowValues = dataStruct.Fields;
            ColumnHeaderMouseClick += ColumnSelected;

            AddMetadata();
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

        public DataStruct DataStruct { get => (DataStruct)Tag; set => Tag = value; }

        public override void Reload()
        {
            base.Refresh();
            RowValues = DataStruct.Fields;
            AddMetadata();
        }

        protected override EditorGridColumnCollection GridColumns => new EditorGridColumnCollection()
        {
            new EditorGridColumn("Name", "Name", typeof(EditorGridTextCell)),
            new EditorGridColumn("Data Type", "DataType", typeof(DataStructEditorGridDataTypeCell)),
            new EditorGridColumn("Is Nullable?", "IsNullable", typeof(EditorGridCheckCell)),
            new EditorGridColumn("Key Types", "KeyTypes", typeof(DataStructEditorGridKeyTypeCell))
        };

        void AddMetadata()
        {
            foreach (DataStructField field in DataStruct.Fields)
            {
                if (field.Metadata != null)
                {
                    foreach (var metadata in field.Metadata)
                    {
                        AddMetadataColumn(metadata.Key);
                        int columnIndex = Columns[metadata.Key].Index;
                        Rows[DataStruct.Fields.IndexOf(field)].Cells[columnIndex].Value = metadata.Value;
                        DataStructEditorGridFieldMetadataCell cell = new DataStructEditorGridFieldMetadataCell();
                        cell.BaseProperty = metadata.Key;
                        cell.SetValue(metadata.Value);
                        Rows[DataStruct.Fields.IndexOf(field)].Cells[columnIndex].Tag = cell;
                    }
                }
            }
        }


        public void AddMetadataColumn(string name)
        {
            if (Columns.Contains(name))
            {
                return;
            }

            Columns.Add(name, name);
            Columns[Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            foreach (DataGridViewRow row in Rows)
            {
                DataGridViewColumn column = Columns[Columns.Count - 1];
                DataGridViewCell gridCell = Rows[row.Index].Cells[column.Index];

                if (gridCell.Tag == null)
                {
                    gridCell.Tag = new DataStructEditorGridFieldMetadataCell() { BaseProperty = column.Name };
                }
            }
        }
    }
}
