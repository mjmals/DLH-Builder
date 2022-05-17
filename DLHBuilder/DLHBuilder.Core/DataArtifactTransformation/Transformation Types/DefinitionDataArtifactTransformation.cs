using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DefinitionDataArtifactTransformation : DataArtifactTransformation
    {
        public override string Title => "Definition";

        public string Definition { get; set; }

        public override string ToString()
        {
            return Definition;
        }
    }
}
