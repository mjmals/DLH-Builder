using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;
using RazorEngineCore;

namespace DLHApp.Build.TemplateRenderers
{
    public class RazorTemplateRenderer : TemplateRenderer
    {
        public override string FileExtension => ".cshtml";

        public override string Render(string templateFile, object baseObject)
        {
            string templateContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Templates", templateFile));

            IRazorEngine engine = new RazorEngine();
            IRazorEngineCompiledTemplate template = engine.Compile(templateContent, builder => {
                builder.AddAssemblyReference(typeof(Project).Assembly);
            });

            string output = template.Run(baseObject);

            return output;
        }
    }
}
