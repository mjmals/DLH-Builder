using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataStages
{
    public abstract class DataStage : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath(string.Empty);

        protected override string GetBasePath(string basePath)
        {
            string output = Path.Combine(string.IsNullOrEmpty(FolderPath) ? string.Empty : FolderPath, string.IsNullOrEmpty(Name) ? string.Empty : Name);

            if(string.IsNullOrEmpty(output))
            {
                return "Data Applications";
            }

            return output;
        }

        public static Type[] StageTypes
        {
            get
            {
                Type baseType = typeof(DataStage);
                return baseType.Assembly.GetTypes().Where(x => x.IsAssignableTo(baseType) && x.IsAbstract == false && x.IsInterface == false).ToArray();
            }
        }

        public static DataStage New(string parentPath, string name)
        {
            return null;
        }

        public static DataStage Load(string path)
        {
            Type[] stgTypes = StageTypes;

            foreach (Type stgType in stgTypes)
            {
                DataStage stg = (DataStage)Activator.CreateInstance(stgType);

                if (path.EndsWith(stg.OutputExtension))
                {
                    DataStage output = (DataStage)JsonConvert.DeserializeObject(File.ReadAllText(path), stgType);
                    output.Name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                    output.SourcePath = Path.GetDirectoryName(path);
                    return output;
                }
            }

            return null;
        }

        public override TemplateModelItem GetTemplateItems()
        {
            TemplateModelItem output = base.GetTemplateItems();

            List<string> refFiles = new List<string>();

            foreach(string file in Directory.GetFiles(SourcePath, "*.ref.json", SearchOption.AllDirectories))
            {
                refFiles.Add(Path.GetDirectoryName(file).Replace(SourcePath + @"\", ""));
            }

            output.Add("ReferenceNames", refFiles.ToArray());

            return output;
        }

        public virtual TemplateReferenceCollection DefaultReferenceTemplates { get; set; }
    }
}
