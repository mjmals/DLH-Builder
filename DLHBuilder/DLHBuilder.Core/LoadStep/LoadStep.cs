using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class LoadStep : ILoadStep
    {
        public Guid ID { get; set; }

        public string Name { get => name; set { name = value; PropertyUpdated?.Invoke(null, null); } }

        private string name { get; set; }

        public CodeDefinitionLanguage Language { get; set; }

        public string Code { get; set; }

        public int Ordinal { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated;
    }
}
