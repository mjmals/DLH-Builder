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
            new EditorGridDropdownColumn("Source Field", "SourceField", typeof(EditorGridDropdownCell), (string[])SourceStruct.Fields.Select(x => x.Name).ToArray()),
            new EditorGridColumn("Output Name", "OutputName", typeof(EditorGridTextCell))
        };

        protected override void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            base.CellUpdated(sender, e);
            Reference.Save();
        }
    }
}
