using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static DataPipelineTask New()
        {
            DataPipelineTask output = new DataPipelineTask();
            output.IncludedItems = new List<string>();
            output.ExcludedItems = new List<string>();
            output.Templates = new TemplateReferenceCollection();

            return output;
        }
    }
}
