using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class SchemaItemEditor : Editor
    {
        public SchemaItemEditor(DataArtifactSchemaItem item)
        {
            Item = item;
        }

        DataArtifactSchemaItem Item { get; set; }
    }
}
