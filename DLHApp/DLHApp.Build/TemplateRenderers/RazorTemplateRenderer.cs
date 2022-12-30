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

        public override string Render(string templateFile, object baseObject, out string fileName)
        {
            string templateContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Templates", templateFile));

            IRazorEngine engine = new RazorEngine();
            IRazorEngineCompiledTemplate template = engine.Compile(templateContent, builder => {
                builder.AddAssemblyReference(typeof(Project).Assembly);
            });

            fileName = "Error.txt";

            try
            {
                string compiled = template.Run(baseObject);
                string output = string.Empty;

                foreach(string line in compiled.Split("\n"))
                {
                    if(line.StartsWith("TemplateHeaderFileName:"))
                    {
                        fileName = line.Replace("TemplateHeaderFileName:", "").TrimStart().TrimEnd().Replace("\n", "");
                        continue;
                    }

                    output += string.IsNullOrEmpty(output) ? line : string.Format("\n{0}", line);
                }

                return output.TrimStart().TrimEnd();
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
