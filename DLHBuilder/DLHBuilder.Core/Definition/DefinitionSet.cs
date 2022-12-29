using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class DefinitionSet: IDefinitionSet
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

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }

        protected virtual void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
