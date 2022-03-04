using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4;
using System.IO;
using Antlr4.StringTemplate;
using System.Reflection;

namespace DLHBuilder.Generator
{
    public class ScriptEngine
    {
        public ScriptEngine(string template, params object[] baseobjects)
        {
            Template = template;
            BaseObjects = baseobjects;
        }

        string Template { get; set; }

        object[] BaseObjects { get; set; }

        string TemplateContent()
        {
            string[] resources = this.GetType().Assembly.GetManifestResourceNames();
            string templatefile = resources.FirstOrDefault(x => x == string.Format("DLHBuilder.Generator.Templates.{0}.st", Template));
            
            StreamReader reader = new StreamReader(this.GetType().Assembly.GetManifestResourceStream(templatefile));
            return reader.ReadToEnd();
        }

        public string Render()
        {
            char delimiter = '$';
            string content = TemplateContent();
            Template output = new Template(content, delimiter, delimiter);

            foreach(object baseobject in BaseObjects)
            {
                if(content.Contains(string.Format("{0}{1}.", delimiter, baseobject.GetType().Name)))
                {
                    output.Add(baseobject.GetType().Name, baseobject);
                }

                foreach(PropertyInfo property in baseobject.GetType().GetProperties())
                {
                    string propertyfullname = string.Format("{0}_{1}", baseobject.GetType().Name, property.Name);

                    if(content.Contains(propertyfullname))
                    {
                        output.Add(propertyfullname, property.GetValue(baseobject));
                    }
                }
            }

            return output.Render();
        }
    }
}
