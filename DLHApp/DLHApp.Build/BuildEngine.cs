using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DLHApp.Model;
using DLHApp.Model.Templates;
using DLHApp.Model.BuildProfiles;
using DLHApp.Build.TemplateRenderers;

namespace DLHApp.Build
{
    public class BuildEngine
    {
        public BuildEngine(string buildProfile)
        {
            Profile = BuildProfile.Load(buildProfile);
        }

        BuildProfile Profile { get; set; }

        public void Run()
        {
            string[] templateFiles = GetTemplateFiles();

            Type[] templateCollectionTypes = typeof(TemplateReferenceCollection).Assembly.GetTypes()
                .Where(x => x.GetFields().Where(y => y.FieldType == typeof(TemplateReferenceCollection)).Count() > 0 && x.IsAbstract == false && x.IsInterface == false)
                .ToArray();

            foreach(Type modelType in templateCollectionTypes)
            {
                IModelItem modelItem = (IModelItem)Activator.CreateInstance(modelType);

                foreach(string file in GetFiles(modelItem.BasePath))
                {
                    IModelItem model = (IModelItem)modelType.GetMethod("Load").Invoke(null, new[] { file });
                    TemplateReferenceCollection templates = (TemplateReferenceCollection)modelType.GetFields().FirstOrDefault(x => x.FieldType == typeof(TemplateReferenceCollection)).GetValue(model);

                    BuildEngineOutputWriter writer = new BuildEngineOutputWriter(Profile, templates, templateFiles, model);
                    writer.Run();
                }
            }

            Console.WriteLine("Build Successful");
        }

        string[] GetFiles(string path)
        {
            List<string> output = new List<string>();

            SearchFiles(path, output);

            return output.ToArray();
        }

        void SearchFiles(string path, List<string> fileList)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                fileList.Add(file);
            }

            foreach(string subDir in Directory.GetDirectories(path))
            {
                SearchFiles(subDir, fileList);
            }
        }

        string[] GetTemplateFiles()
        {
            List<string> output = new List<string>();
            List<string> templatesFull = new List<string>();

            string templatePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            SearchFiles(templatePath, templatesFull);

            foreach(string template in templatesFull)
            {
                output.Add(template.Replace(templatePath + @"\", ""));
            }

            return output.ToArray();
        }

        void WriteTemplateOutput()
        {
            
        }
    }
}
