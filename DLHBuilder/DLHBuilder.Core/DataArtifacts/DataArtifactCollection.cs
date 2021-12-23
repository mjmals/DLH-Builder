using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactCollection
    {
        public DataArtifactCollection()
        {

        }

        List<DataArtifact> _artifacts = new List<DataArtifact>();

        [JsonIgnore]
        public DataArtifact[] Artifacts
        {
            get
            {
                return _artifacts.ToArray();
            }
            set
            {
                _artifacts = ((DataArtifact[])value).ToList();
            }
        }

        public string Name { get; set; }

        public void Add(DataArtifact artifact)
        {
            _artifacts.Add(artifact);
        }

        public DataArtifact CreateDataArtifact(string name)
        {
            DataArtifact output = new DataArtifact();
            output.Name = name;

            _artifacts.Add(output);

            return output;
        }

        internal void Save(string path)
        {
            path = Path.Combine(path, Name);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = Path.Combine(path, "Group.json");

            MetadataController.SaveObject(this, filepath);

            string artifactpath = Path.Combine(path, "Data Artifacts");

            if (!Directory.Exists(artifactpath))
            {
                Directory.CreateDirectory(artifactpath);
            }

            foreach (DataArtifact artifact in Artifacts)
            {
                artifact.Save(artifactpath);
            }
        }
    }
}
