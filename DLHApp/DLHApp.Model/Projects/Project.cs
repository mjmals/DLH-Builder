using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.Projects
{
    public class Project : ModelItem, IModelItem
    {
        [JsonIgnore]
        public new string BasePath { get; set; }

        public static void Initialize()
        {
            File.CreateText(Path.Combine(Environment.CurrentDirectory, "project.json"));

            Type[] modelTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(IModelItem)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach (Type modelType in modelTypes)
            {
                IModelItem model = (IModelItem)Activator.CreateInstance(modelType);

                if (!Directory.Exists(model.BasePath) && !string.IsNullOrEmpty(model.BasePath))
                {
                    Directory.CreateDirectory(model.BasePath);
                }
            }
        }

        public static Project Load(string path)
        {
            string projectName = new DirectoryInfo(path).Parent.Name;
            string projectRoot = new DirectoryInfo(path).Parent.FullName;

            return new Project()
            {
                Name = projectName,
                BasePath = projectRoot
            };
        }
    }
}
