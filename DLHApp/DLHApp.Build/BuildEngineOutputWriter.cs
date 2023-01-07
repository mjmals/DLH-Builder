using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.BuildProfiles;
using DLHApp.Build.TemplateRenderers;

namespace DLHApp.Build
{
    internal class BuildEngineOutputWriter
    {
        public BuildEngineOutputWriter(BuildProfile profile, TemplateReferenceCollection templateRefs, string[] templates, TemplateModelItem templateItems)
        {
            Profile = profile;
            TemplateRefs = templateRefs;
            Templates = templates;
            TemplateItems = templateItems;
        }

        BuildProfile Profile { get; set; }

        TemplateReferenceCollection TemplateRefs { get; set; }

        string[] Templates { get; set; }

        TemplateModelItem TemplateItems { get; set; }

        public void Run()
        {
            foreach (BuildProfileStage stage in Profile.Stages)
            {
                string[] templateFiles = GetTemplates(stage);

                foreach(string templateFile in templateFiles)
                {
                    if (TemplateRefs.Exists(x => templateFile.StartsWith(x)))
                    {
                        OutputTemplate(templateFile, stage);
                    }
                }
            }
        }

        string[] GetTemplates(BuildProfileStage stage)
        {
            List<string> output = new List<string>();

            foreach(string templateRef in stage.Templates)
            {
                output.AddRange(Templates.Where(x => x.StartsWith(templateRef)));
            }

            return output.ToArray();
        }


        void OutputTemplate(string templateFile, BuildProfileStage stage)
        {
            TemplateRenderer renderer = TemplateRenderer.GetRenderer(templateFile);
            string outputFileName = string.Empty;
            string compiledTemplate = renderer.Render(templateFile, TemplateItems, out outputFileName);
            string outputPath = Path.Combine(Profile.UserConfig.TargetFolder, stage.OutputPath, outputFileName);

            WriteOutputFile(outputPath, compiledTemplate);
        }

        void WriteOutputFile(string outputPath, string outputContent)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            }

            using (FileStream fs = new FileStream(outputPath, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(outputContent);
                }
            }
        }
    }
}
