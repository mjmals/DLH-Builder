using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DLHBuilder.Config;

namespace DLHBuilder.Build
{
    public abstract class BuildEngine : IBuildEngine
    {
        public virtual string Name { get; set; }

        public virtual string OutputPath
        {
            get
            {
                return ConfigController.GetValue(ConfigItem);
            }
            set
            {
                ConfigController.SetValue(ConfigItem, value);
            }
        }

        public virtual string ConfigItem { get; set; }

        public virtual BuildEngineOutputCollection Outputs { get; set; }

        public virtual void Run(Project project)
        {
            foreach(IBuildEngineOutput output in Outputs)
            {
                string buildPath = Path.Combine(OutputPath, output.OutputFolder);

                if(!Directory.Exists(buildPath))
                {
                    Directory.CreateDirectory(buildPath);
                }

                object[] buildObjecs = GetBuildObjects(project, output);
            }
        }

        object[] GetBuildObjects(Project project, IBuildEngineOutput buildOutput)
        {
            List<object> output = new List<object>();

            foreach (string templatePath in buildOutput.TemplatePaths)
            {
                output.AddRange(project.Connections.Where(x => x.ScriptTemplates.Exists(y => y.Template.Contains(templatePath))));
                output.AddRange(project.Artifacts.Where(x => x.ScriptTemplates.Exists(y => y.Template.Contains(templatePath))));
                
                foreach(DataStage stage in project.Applications.SelectMany(x => x.Stages).Where(x => x.ArtifactDefaultScriptTemplates.Exists(y => y.Template.Contains(templatePath))))
                {
                    output.AddRange(stage.ArtifactReferences);
                }
            }

            return output.ToArray();
        }
    }
}
