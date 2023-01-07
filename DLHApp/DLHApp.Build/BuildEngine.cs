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
                .Where(x => x.IsAbstract == false && x.IsInterface == false)
                .Where(x => x.GetProperties().Where(y => y.PropertyType == typeof(TemplateReferenceCollection)).Count() > 0)
                .Where(x => x != typeof(BuildProfile))
                .ToArray();

            foreach(Type modelType in templateCollectionTypes)
            {
                IModelItem modelItem = (IModelItem)Activator.CreateInstance(modelType);

                if(string.IsNullOrEmpty(modelItem.BasePath))
                {
                    continue;
                }

                foreach(string file in GetFiles(modelItem.BasePath, modelItem.OutputExtension))
                {
                    MethodInfo loadMethod = modelType.GetMethod("Load") == null ? modelType.BaseType.GetMethod("Load") : modelType.GetMethod("Load");
                    IModelItem model = (IModelItem)loadMethod.Invoke(null, new[] { file });
                    TemplateModelItem templateItems = model.GetTemplateItems();
                    TemplateReferenceCollection templates = (TemplateReferenceCollection)modelType.GetProperties().FirstOrDefault(x => x.PropertyType == typeof(TemplateReferenceCollection)).GetValue(model);

                    if (templates.Count() > 0)
                    {
                        BuildEngineOutputWriter writer = new BuildEngineOutputWriter(Profile, templates, templateFiles, templateItems);
                        writer.Run();
                    }
                }
            }

            Console.WriteLine("Build Successful");
        }

        string[] GetFiles(string path, string searchPattern)
        {
            List<string> output = new List<string>();

            searchPattern = "*" + searchPattern;

            SearchFiles(path, searchPattern, output);

            return output.ToArray();
        }

        void SearchFiles(string path, string searchPattern, List<string> fileList)
        {
            foreach (string file in Directory.GetFiles(path, searchPattern))
            {
                fileList.Add(file);
            }

            foreach(string subDir in Directory.GetDirectories(path))
            {
                SearchFiles(subDir, searchPattern, fileList);
            }
        }

        string[] GetTemplateFiles()
        {
            List<string> output = new List<string>();
            List<string> templatesFull = new List<string>();

            string templatePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            SearchFiles(templatePath, string.Empty, templatesFull);

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
