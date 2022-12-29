using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class ScriptTemplate
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ScriptTemplateType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ScriptTemplateEngineType Engine { get; set; }

        public List<string> Hierarchy = new List<string>();

        [JsonIgnore]
        public string Content { get; set; }

        public string Path()
        {
            return string.Join('.', Hierarchy);
        }
    }
}
