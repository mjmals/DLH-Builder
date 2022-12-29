using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridObjectCell : DataGridViewTextBoxCell, IEditorGridCell
    {
        public object BaseObject { get; set; }

        public string PropertyName { get; set; }

        public object PropertyValue { get => Value; set => Value = value; }

        public bool AllowEdit => false;

        public Type ColumnType => typeof(DataGridViewTextBoxColumn);

        public virtual EditorGridPanel Panel { get; set; }

        public void SetProperties(object baseObject, string propertyName, object propertyValue, EditorGridDatasourceDefinition dataSource = null)
        {
            BaseObject = baseObject;
            PropertyName = propertyName;
            Value = propertyValue;
        }

        public void ProcessCellUpdate()
        {
            EditorGridPropertyUpdater.Run(BaseObject, PropertyName, PropertyValue);
        }
    }
}
