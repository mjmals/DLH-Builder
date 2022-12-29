using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class ScriptTemplateReference
    {
        public string Template 
        {
            get => template;
            set
            {
                template = value;
                PropertyUpdated?.Invoke(null, null) ;
            }
        }

        private string template { get; set; }

        public string DisplayName 
        { 
            get => displayname; 
            set
            {
                displayname = value;
                PropertyUpdated?.Invoke(null, null);
            }
        }

        private string displayname { get; set; }

        [JsonIgnore]
        public EventHandler PropertyUpdated { get; set; }
    }
}
