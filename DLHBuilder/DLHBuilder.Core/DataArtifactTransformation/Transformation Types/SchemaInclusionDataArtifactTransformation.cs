using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SchemaInclusionDataArtifactTransformation : IDataArtifactTransformation
    {
        public Guid ID { get; set; }

        public string Title => "Schema Item Inclusion";

        public Guid ReferencedObjectID { get; set; }

        public bool Include { get; set; }
    }
}
