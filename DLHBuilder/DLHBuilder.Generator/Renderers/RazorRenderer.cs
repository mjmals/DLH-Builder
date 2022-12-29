using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RazorEngineCore;

namespace DLHBuilder.Generator.Renderers
{
    class RazorRenderer : ITemplateRenderer
    {
        public RazorRenderer(ScriptTemplate template, params object[] baseObjects)
        {
            Template = template;
            BaseObjects = baseObjects;
        }

        ScriptTemplate Template { get; set; }

        object[] BaseObjects { get; set; }

        public string Render()
        {
            string headerEndTag = "##ENDHEADER";
            string content = Template.Content.ToUpper().Contains(headerEndTag) ? Template.Content.Substring(Template.Content.ToUpper().IndexOf(headerEndTag) + headerEndTag.Length) : Template.Content;

            IRazorEngine engine = new RazorEngine();
            IRazorEngineCompiledTemplate template = engine.Compile(content, builder => 
            {
                builder.AddAssemblyReference(typeof(Project).Assembly);
                //builder.AddAssemblyReference(typeof(System.Collections.ArrayList).Assembly);
                builder.AddAssemblyReferenceByName("System.Collections");
            });

            string output = CleanCode(template.Run(BaseObjects[0]));
            CleanTempFiles();

            return output;
        }

        private string CleanCode(string code)
        {
            code = code.Replace("&#39;", "'");
            code = code.Replace("&gt;", ">");
            code = code.Replace("&lt;", "<");
            code = code.Replace("&#160;", " ");

            return code.TrimStart();
        }

        private void CleanTempFiles()
        {
            foreach (string dir in Directory.GetDirectories(Path.GetTempPath(), "RazorEngine_*"))
            {
                Directory.Delete(dir, true);
            }
        }
    }
}
