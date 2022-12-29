using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    public interface IEditorGridCell
    {
        public object BaseObject { get; set; }

        public string PropertyName { get; }

        public object PropertyValue { get; set; }

        public bool AllowEdit { get; }

        public Type ColumnType { get; }

        public void SetProperties(object baseObject, string propertyName, object propertyValue, EditorGridDatasourceDefinition dataSource = null);

        public void ProcessCellUpdate();
    }
}
