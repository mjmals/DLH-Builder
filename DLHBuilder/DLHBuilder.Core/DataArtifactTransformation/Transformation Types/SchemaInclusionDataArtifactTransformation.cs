using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SchemaInclusionDataArtifactTransformation : DataArtifactTransformation
    {
        public override string Title => "Schema Item Inclusion";

        public bool Include { get; set; }

        public override string ToString()
        {
            return Include.ToString();
        }
    }
}
