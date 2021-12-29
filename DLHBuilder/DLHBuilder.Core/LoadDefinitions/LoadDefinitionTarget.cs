using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public abstract class LoadDefinitionTarget
    {
        public LoadDefinitionTarget(DataLayerType targetlayer)
        {
            Layer = targetlayer;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataFileFormat Type { get => _type; }

        protected DataFileFormat _type { get; set; }

        public DataLayerType Layer { get; set; }

        public virtual string FullPath()
        {
            return null;
        }
    }
}
