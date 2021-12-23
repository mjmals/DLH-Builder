using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifact : IDataArtifact
    {
        public string Name { get; set; }

        public DataArtifactPropertyCollection ItemDefinitions { get; set; }

        public LoadDefinitionCollection LoadDefinitions { get; set; }

        public void Save(string path)
        {
            path = Path.Combine(path, Name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
