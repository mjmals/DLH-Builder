using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class Project
    {
        public string Name { get; set; }

        List<DataArtifactCollection> _artifactcollections = new List<DataArtifactCollection>();

        public DataArtifactCollection[] DataArtifactCollections
        {
            get => _artifactcollections.ToArray();
            set => _artifactcollections = ((DataArtifactCollection[])value).ToList();
        }

        public DataArtifactCollection CreateDataArtfactCollection(string name)
        {
            DataArtifactCollection output = new DataArtifactCollection();
            output.Name = name;

            _artifactcollections.Add(output);

            return output;
        }
    }
}
