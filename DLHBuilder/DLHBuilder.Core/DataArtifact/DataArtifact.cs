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

        public string Description { get; set; }

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

        public static DataArtifact New()
        {
            DataArtifact output = new DataArtifact();
            output.Name = "<New Artifact>";

            return output;
        }
    }
}
