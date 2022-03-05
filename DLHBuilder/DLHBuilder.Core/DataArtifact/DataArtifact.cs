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
        public string Name { get; set; }

        public string Description { get; set; }

        public DataArtifactSchemaCollection Schemas 
        { 
            get
            {
                if(schemas == null)
                {
                    schemas = new DataArtifactSchemaCollection();
                }
                return schemas;
            }
            set => schemas = value; 
        }

        private DataArtifactSchemaCollection schemas { get; set; }

        public static DataArtifact New()
        {
            DataArtifact output = new DataArtifact();
            output.Name = "<New Artifact>";

            return output;
        }
    }
}
