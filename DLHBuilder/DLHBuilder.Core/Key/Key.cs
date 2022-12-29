using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class Key
    {
        public Guid ID { get; set; }

        public string Name 
        { 
            get => name; 
            set
            {
                name = value;
                OnPropertyUpdated(name);
            }
        }

        private string name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyType Type { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }

        void OnPropertyUpdated(object value)
        {
            PropertyUpdated?.Invoke(this, null);
        }
    }
}
