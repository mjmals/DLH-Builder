using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DLHBuilder
{
    public abstract class DataStage : IDataStage
    {
        [Browsable(false)]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public DataArtifactCollection Artifacts
        {
            get
            {
                if(artifacts == null)
                {
                    artifacts = new DataArtifactCollection();
                }
                return artifacts;
            }
            set => artifacts = value;
        }

        private DataArtifactCollection artifacts { get; set; }
    }
}
