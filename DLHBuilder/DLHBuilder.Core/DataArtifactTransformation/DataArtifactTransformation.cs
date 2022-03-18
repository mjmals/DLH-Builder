using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactTransformation
    {
        public Guid DataSourceID { get; set; }

        // to be de-commissioned
        public Guid DataStageID { get; set; }

        public string Definition { get; set; }
    }
}
