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
