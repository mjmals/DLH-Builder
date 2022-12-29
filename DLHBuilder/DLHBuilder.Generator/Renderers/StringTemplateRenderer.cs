using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.StringTemplate;
using System.Reflection;

namespace DLHBuilder.Generator.Renderers
{
    class StringTemplateRenderer : ITemplateRenderer
    {
        public StringTemplateRenderer(ScriptTemplate template, params object[] baseObjects)
        {
            Template = template;
            BaseObjects = baseObjects;
        }

        ScriptTemplate Template { get; set; }

        object[] BaseObjects { get; set; }

        public string Render()
        {
            char delimiter = '$';
            string content = Template.Content;
            Template output = new Template(content, delimiter, delimiter);

            foreach (object baseobject in BaseObjects)
            {
                if (content.Contains(string.Format("{0}{1}.", delimiter, baseobject.GetType().Name)))
                {
                    output.Add(baseobject.GetType().Name, baseobject);
                }
            }

            return output.Render();
        }
    }
}
