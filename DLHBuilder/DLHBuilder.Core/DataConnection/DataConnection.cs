using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public abstract class DataConnection : IDataConnection
    {
        [Browsable(false)]
        public Guid ID { get; set; }

        public virtual string Name { get; set; }

        public ScriptTemplateReferenceCollection ScriptTemplates
        {
            get => scriptTemplates = scriptTemplates == null ? new ScriptTemplateReferenceCollection() : scriptTemplates;
            set => scriptTemplates = value;
        }

        private ScriptTemplateReferenceCollection scriptTemplates { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }

        public void OnPropertyUpdated(object item, object oldValue, object newValue)
        {
            PropertyUpdated?.Invoke(item, null);
        }
    }
}
