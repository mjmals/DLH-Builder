using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactSchemaItemEventArgs : EventArgs
    {
        public DataArtifactSchemaItemEventArgs(DataArtifactSchemaItem item)
        {
            Item = item;
        }

        public DataArtifactSchemaItem Item { get; set; }
    }
}
