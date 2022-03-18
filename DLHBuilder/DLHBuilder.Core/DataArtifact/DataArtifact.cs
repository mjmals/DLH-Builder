using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifact
    {
        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(null, null);
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyUpdated();
            }
        }

        private string name { get; set; }

        public List<string> ArtifactNamespace
        {
            get
            {
                if(artifactnamespace == null)
                {
                    artifactnamespace = new List<string>();
                }
                return artifactnamespace;
            }
            set
            {
                OnPropertyUpdated();
                artifactnamespace = value;
            }
        }

        private List<string> artifactnamespace { get; set; }

        [JsonIgnore]
        public string ArtifactPath => string.Join('.', ArtifactNamespace);

        [JsonIgnore]
        public string FullName => ArtifactNamespace.Count == 0 ? Name : string.Format("{0}.{1}", ArtifactPath, Name);

        public string Description { get; set; }

        [JsonIgnore]
        public DataSourceCollection DataSources
        {
            get
            {
                if(datasources == null)
                {
                    datasources = new DataSourceCollection();
                }
                return datasources;
            }
            set => datasources = value;
        }

        private DataSourceCollection datasources { get; set; }

        [JsonIgnore]
        public DataArtifactSchemaItemCollection Schema
        { 
            get
            {
                if(schema == null)
                {
                    schema = new DataArtifactSchemaItemCollection();
                }
                return schema;
            }
            set => schema = value; 
        }

        private DataArtifactSchemaItemCollection schema { get; set; }

        public DataArtifactTransformationCollection Transformations
        {
            get
            {
                if(transformations == null)
                {
                    transformations = new DataArtifactTransformationCollection();
                }
                return transformations;
            }
            set => transformations = value;
        }

        private DataArtifactTransformationCollection transformations { get; set; }

        public static DataArtifact New(string path = null)
        {
            DataArtifact output = new DataArtifact();
            output.Name = "<New Artifact>";

            if (!string.IsNullOrEmpty(path))
            {
                output.ArtifactNamespace.AddRange(path.Split('.'));
            }

            return output;
        }
    }
}
