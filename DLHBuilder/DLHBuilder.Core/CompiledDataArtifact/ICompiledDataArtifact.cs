using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface ICompiledDataArtifact
    {
        public DataArtifact Artifact { get; set; }

        public IDataStage Stage { get; set; }

        public DataArtifactReference Reference { get; set; }

        public string Name { get; }

        public string[] Path { get; set; }

        public string FullPath();

        public ICompiledSchemaItem[] Schema { get; set; }
    }
}
