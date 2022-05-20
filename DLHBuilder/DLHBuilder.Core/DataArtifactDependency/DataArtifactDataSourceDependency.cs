using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactDataSourceDependency : IDataArtifactDependency
    {
        public string Name { get; set; }

        public Guid ArtifactID { get; set; }

        public Guid DataSourceID { get; set; }
    }
}
