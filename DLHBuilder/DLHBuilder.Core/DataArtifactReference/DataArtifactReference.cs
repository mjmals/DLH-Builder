using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactReference
    {
        public Guid ID { get; set; }

        public Guid DataArtifactID { get; set; }

        public List<string> Path { get; set; }

        public string FullPath
        {
            get
            {
                List<string> output = new List<string>();
                output.AddRange(Path);
                output.Add(ID.ToString());

                return string.Join('.', output);
            }
        }

        [JsonIgnore]
        public string Name
        {
            get
            {
                return ID.ToString();
            }
        }

        [JsonIgnore]
        public DataArtifact ReferencedArtifact { get; set; }

        [JsonIgnore]
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
    }
}
