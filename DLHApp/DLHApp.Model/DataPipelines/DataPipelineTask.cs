using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataPipelines
{
    public class DataPipelineTask : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath(string.Empty);

        protected override string GetBasePath(string basePath)
        {
            if(!string.IsNullOrEmpty(FolderPath))
            {
                return FolderPath;
            }

            return string.Empty;
        }

        public override string OutputExtension => "dpltsk.json";

        public List<string> IncludedItems { get; set; }

        public List<string> ExcludedItems { get; set; }

        public int Order { get; set; }

        public override TemplateModelItem GetTemplateItems()
        {
            TemplateModelItem output = base.GetTemplateItems();

            List<string> includedItems = new List<string>();

            foreach(string includedItem in IncludedItems)
            {
                foreach(string file in Directory.GetFiles(includedItem, "*.json", SearchOption.AllDirectories))
                {
                    if(ExcludedItems != null && ExcludedItems.Where(x => file.StartsWith(x)).Count() > 0)
                    {
                        continue;
                    }

                    includedItems.Add(file);
                }
            }

            output.Add("IncludedItems", includedItems);

            return output;
        }

        public static DataPipelineTask New()
        {
            DataPipelineTask output = new DataPipelineTask();
            output.IncludedItems = new List<string>();
            output.ExcludedItems = new List<string>();
            output.Templates = new TemplateReferenceCollection();

            return output;
        }

        public static DataPipelineTask Load(string fileName)
        {
            DataPipelineTask output = JsonConvert.DeserializeObject<DataPipelineTask>(File.ReadAllText(fileName));
            output.Name = Path.GetFileNameWithoutExtension(fileName).Replace(".dpltsk", "");
            output.FolderPath = Path.GetDirectoryName(fileName);
            return output;
        }
    }
}
