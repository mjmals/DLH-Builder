using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactSchemaItemMapping
    {
        public DataArtifactSchemaItemMapping(DataArtifactSchemaItem source, DataArtifactSchemaItem target)
        {
            Source = source;
            Target = target;
        }

        public DataArtifactSchemaItem Source { get; set; }

        public DataArtifactSchemaItem Target { get; set; }
    }
}
