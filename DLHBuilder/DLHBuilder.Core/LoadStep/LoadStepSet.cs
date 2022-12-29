using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class LoadStepSet : BuilderCollection<ILoadStep>
    {
        public Guid ID { get; set; }

        public string Name { get => name; set { name = value; PropertyUpdated?.Invoke(null, null); } }

        private string name { get; set; }

        public Type SetType { get; set; }

        public int Ordinal { get; set; }

        internal override void Save(string path)
        {
            
        }

        internal override void Load(string path)
        {
            
        }

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }

        public new void Add(ILoadStep step)
        {
            base.Add(step);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Ordinal = i + 1;
            }
        }
    }
}
