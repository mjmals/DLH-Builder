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
        }

        public DataStruct DataStruct { get; set; }

        protected override EditorGridColumnCollection GridColumns => new EditorGridColumnCollection()
        {
            new EditorGridColumn("Name", "Name", typeof(EditorGridTextCell)),
            new EditorGridColumn("Data Type", "DataType", typeof(EditorGridTextCell)),
            new EditorGridColumn("Is Nullable?", "IsNullable", typeof(EditorGridCheckCell)),
            new EditorGridColumn("Key Types", "KeyTypes", typeof(DataStructEditorGridKeyTypeCell))
        };
    }
}
