using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids.DataStructReferences
{
    internal class DataStructReferenceEditorGrid : EditorGrid
    {
        public DataStructReferenceEditorGrid(DataStructReference reference, DataStruct sourceStruct)
        {
            Reference = reference;
            SourceStruct = sourceStruct;
            RowValues = Reference.Fields;
        }

        DataStructReference Reference { get; set; }

        DataStruct SourceStruct { get; set; }

        protected override EditorGridColumnCollection GridColumns => new EditorGridColumnCollection()
        {
            new EditorGridColumn("Source Field", "SourceField", typeof(EditorGridTextCell)),
            new EditorGridColumn("Output Name", "OutputName", typeof(EditorGridTextCell))
        };
    }
}
