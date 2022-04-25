using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class ScriptTemplate
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public ScriptTemplateType Type { get; set; }

        public List<string> Hierarchy = new List<string>();

        [JsonIgnore]
        public string Content { get; set; }

        public string Path()
        {
            return string.Join('.', Hierarchy);
        }
    }
}
