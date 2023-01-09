using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataStructReferences
{
    public class DataStructReference : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath(string.Empty);

        protected override string GetBasePath(string basePath)
        {
            string output = Path.Combine(string.IsNullOrEmpty(FolderPath) ? string.Empty : FolderPath, string.IsNullOrEmpty(Name) ? string.Empty : Name);

            if (string.IsNullOrEmpty(output))
            {
                return "Data Applications";
            }

            return output;
        }

        public override string OutputExtension => "ref.json";

        public string SourceDataStruct { get; set; }

        public List<string> Fields { get; set; }

        public override void Save()
        {
            base.Save();

            string[] subDirs = new string[] { "Definitions" };

            foreach (string subDir in subDirs)
            {
                string dirName = Path.Combine(Path.GetDirectoryName(OutputPath()), subDir);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        public static DataStructReference New()
        {
            DataStructReference output = new DataStructReference();
            output.SourceDataStruct = string.Empty;
            output.Fields = new List<string>();

            return output;
        }

        public static DataStructReference Load(string path)
        {
            DataStructReference output = JsonConvert.DeserializeObject<DataStructReference>(File.ReadAllText(path));

            return output;
        }
    }
}
