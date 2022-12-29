using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace DLHBuilder
{
    public class DataArtifactSchemaItem
    {
        public Guid ID { get; set; }

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

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IDataType DataType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataArtifactSchemaItemKeyType KeyType { get; set; }

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

        private DataArtifactTransformationCollection transformations;

        public bool IsNullable { get; set; }

        public int Ordinal { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(null, null);
        }

        public static DataArtifactSchemaItem New()
        {
            DataArtifactSchemaItem output = new DataArtifactSchemaItem();
            output.Name = "<New Schema Item>";
            return output;
        }
    }
}
