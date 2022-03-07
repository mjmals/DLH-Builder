using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class SchemaItemGrid : DataGridView
    {
        public SchemaItemGrid(DataArtifactSchemaItemCollection schema)
        {
            Columns.AddRange(new SchemaItemGridColumnCollection().ToArray());
        }
    }
}
