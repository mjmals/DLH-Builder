using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    public class EditorGridColumnDefinition
    {
        public EditorGridColumnDefinition(string displayName, string propertyName, Type cellType, EditorGridDatasourceDefinition dataSource = null)
        {
            DisplayName = displayName;
            PropertyName = propertyName;
            CellType = cellType;
            Datasource = dataSource;
        }

        public string DisplayName { get; }

        public string PropertyName { get; }

        public Type CellType { get; }

        public EditorGridDatasourceDefinition Datasource { get; }
    }
}
