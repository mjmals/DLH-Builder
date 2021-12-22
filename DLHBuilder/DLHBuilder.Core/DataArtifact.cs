using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifact : IDataArtifact
    {
        public string Name { get; set; }

        public LoadDefinitionCollection LoadDefinitions { get; set; }
    }
}
