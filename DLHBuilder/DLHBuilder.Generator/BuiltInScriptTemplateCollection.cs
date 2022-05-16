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
        Dictionary<string, ScriptTemplateEngineType> extensions = new Dictionary<string, ScriptTemplateEngineType>()
        {
            { ".st", ScriptTemplateEngineType.StringTemplate },
            { ".cshtml", ScriptTemplateEngineType.Razor }
        };

        public BuiltInScriptTemplateCollection()
        {
            string[] resources = this.GetType().Assembly.GetManifestResourceNames().Where(x => extensions.ContainsKey(Path.GetExtension(x))).ToArray();

            foreach(string resource in resources)
            {
                string extension = Path.GetExtension(resource);
                string templatepath = resource.Replace("DLHBuilder.Generator.Templates.", "").Replace(extension, "");
                string path = templatepath.Substring(0, templatepath.LastIndexOf("."));
                string templatename = templatepath.Substring(templatepath.LastIndexOf(".") + 1);
                StreamReader reader = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(resource));

                ScriptTemplate template = new ScriptTemplate();
                template.ID = Guid.NewGuid();
                template.Name = templatename;
                template.Type = ScriptTemplateType.BuiltIn;
                template.Hierarchy = ("BuiltIn." + path).Split('.').ToList();
                template.Engine = extensions[extension];
                template.Content = reader.ReadToEnd();

                Add(template);
            }
        }
    }
}
