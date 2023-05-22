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

            AddMetadata();
        }

        public DataStruct DataStruct { get; set; }

        protected override EditorGridColumnCollection GridColumns => new EditorGridColumnCollection()
        {
            new EditorGridColumn("Name", "Name", typeof(EditorGridTextCell)),
            new EditorGridColumn("Data Type", "DataType", typeof(EditorGridTextCell)),
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
                    }
                }
            }
        }


        void AddMetadataColumn(string name)
        {
            if (Columns.Contains(name))
            {
                return;
            }

            Columns.Add(name, name);
        }

    }
}
