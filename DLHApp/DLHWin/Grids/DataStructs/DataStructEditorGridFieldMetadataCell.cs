using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGridFieldMetadataCell : EditorGridCell
    {
        public override Type ColumnType => typeof(DataGridViewTextBoxColumn);

        public override Type CellType => typeof(DataGridViewTextBoxCell);

        public override void UpdateBaseProperty(object baseObject)
        {
            DataStructField field = (DataStructField)baseObject;

            if(field.Metadata == null)
            {
                field.Metadata = new DataStructFieldMetadataCollection();
            }

            if(field.Metadata.ContainsKey(BaseProperty))
            {
                field.Metadata[BaseProperty] = Value.ToString();
            }
            else
            {
                field.Metadata.Add(BaseProperty, Value.ToString());
            }
        }
    }
}
