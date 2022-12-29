using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class Definition : IDefinition
    {
        public Guid ID { get; set; }

        public Guid DefinitionSetID { get; set; }

        [JsonIgnore]
        public string DefinitionSetName { get; set; }
    }
}
