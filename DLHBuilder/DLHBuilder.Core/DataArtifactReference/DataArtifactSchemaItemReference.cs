using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactSchemaItemReference
    {
        public Guid ID { get; set; }

        public Guid SchemaItemID { get; set; }

        [JsonIgnore]
        public DataArtifactSchemaItem ReferencedSchemaItem { get; set; }

        public CodeDefinitionCollection Definitions
        {
            get
            {
                if(definitions == null)
                {
                    definitions = new CodeDefinitionCollection();
                }
                return definitions;
            }
            set => definitions = value;
        }

        private CodeDefinitionCollection definitions { get; set; }

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
