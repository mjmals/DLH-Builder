using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class KeyContainer
    {
        public Guid ID { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyUpdated(value);
            }
        }

        private string name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyContainerType Type { get; set; }

        public KeyCollection Keys
        {
            get
            {
                if(keys == null)
                {
                    keys = new KeyCollection();
                }
                return keys;
            }
            set => keys = value;
        }

        private KeyCollection keys { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }

        void OnPropertyUpdated(object value)
        {
            PropertyUpdated?.Invoke(this, null);
        }
    }
}
