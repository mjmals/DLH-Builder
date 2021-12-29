using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class Dependency
    {
        public string SourceArtifact { get; set; }

        public DataLayerType DataLayer { get; set; }
    }
}
