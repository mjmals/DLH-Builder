using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.EditorGrids
{
    public class DataArtifactSchemaEditorGrid : EditorGrid
    {
        public DataArtifactSchemaEditorGrid(DataArtifactSchemaItem[] baseObjects) : base(baseObjects)
        {

        }

        public override EditorGridColumnDefinition[] ColumnDefinitions
        {
            get
            {
                return new EditorGridColumnDefinition[]
                {
                    new EditorGridColumnDefinition("Name", "Name", typeof(EditorGridTextCell)),
                    new EditorGridColumnDefinition("Data Type", "DataType", typeof(DataArtifactSchemaEditorGridDatatypeCell)),
                    new EditorGridColumnDefinition("Key Type", "KeyType", typeof(EditorGridDropdownCell), new EditorGridDatasourceDefinition(new DataArtifactSchemaItemKeyType())),
                    new EditorGridColumnDefinition("Is Nullable", "IsNullable", typeof(EditorGridCheckCell))
                };
            }
        }
    }
}
