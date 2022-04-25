using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace DLHBuilder
{
    public class BuiltInScriptTemplateCollection : ScriptTemplateCollection
    {
        public BuiltInScriptTemplateCollection()
        {
            string[] resources = this.GetType().Assembly.GetManifestResourceNames().Where(x => x.EndsWith(".st")).ToArray();

            foreach(string resource in resources)
            {
                string templatepath = resource.Replace("DLHBuilder.Generator.Templates.", "").Replace(".st", "");
                string path = templatepath.Substring(0, templatepath.LastIndexOf("."));
                string templatename = templatepath.Substring(templatepath.LastIndexOf(".") + 1);
                StreamReader reader = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(resource));

                ScriptTemplate template = new ScriptTemplate();
                template.ID = Guid.NewGuid();
                template.Name = templatename;
                template.Type = ScriptTemplateType.BuiltIn;
                template.Hierarchy = ("BuiltIn." + path).Split('.').ToList();
                template.Content = reader.ReadToEnd();

                Add(template);
            }
        }
    }
}
