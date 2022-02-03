using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifact
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DataArtifactFormat Format { get; set; }

        public DataArtifactSchemaItemCollection Schema { get; set; }

        public DataArtifactSource Source { get; set; }

        public DataArtifactSchemaItemCollection SourceSchema { get; set; }

        public DataArtifactSchemaItemMapping SchemaMapping { get; set; }
    }
}
