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
