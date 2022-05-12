using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DefinitionDataArtifactTransformation : IDataArtifactTransformation
    {
        public Guid ID { get; set; }

        public string Title => "Definition";

        public Guid ReferencedObjectID { get; set; }

        public string Definition { get; set; }
    }
}
