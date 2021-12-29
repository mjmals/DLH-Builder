using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class Project
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string FilePath { get; set; }

        [JsonIgnore]
        public DataLayerCollection DataLayers = new DataLayerCollection();

        List<DataArtifactCollection> _artifactcollections = new List<DataArtifactCollection>();

        //[JsonIgnore]
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

        public void Save(string path = null)
        {
            if(!string.IsNullOrEmpty(path))
            {
                FilePath = path;
            }

            if(!FilePath.EndsWith(Name))
            {
                FilePath = Path.Combine(FilePath, Name);
            }

            string file = Path.Combine(FilePath, "Project.json");

            MetadataController.SaveObject(this, file);

            string artifactpath = Path.Combine(FilePath, "Data Artifact Groups");

            if(!Directory.Exists(artifactpath))
            {
                Directory.CreateDirectory(artifactpath);
            }

            foreach(DataArtifactCollection group in DataArtifactCollections)
            {
                group.Save(artifactpath);
            }
        }
    }
}
