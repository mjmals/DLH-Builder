using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataPipelines
{
    public class DataPipeline : ModelItem, IModelItem
    {
        public override string BasePath => GetBasePath(string.Empty);

        protected override string GetBasePath(string basePath)
        {
            string output = "Data Pipelines";

            if(!string.IsNullOrEmpty(FolderPath))
            {
                output = Path.Combine(output, FolderPath);
            }

            if(!string.IsNullOrEmpty(Name))
            {
                output = Path.Combine(output, Name);
            }

            return output;
        }

        public override string OutputExtension => "dpl.json";

        public static DataPipeline New()
        {
            DataPipeline output = new DataPipeline();
            output.Templates = new TemplateReferenceCollection();

            return output;
        }

        public static DataPipeline Load(string fileName)
        {
            DataPipeline output = JsonConvert.DeserializeObject<DataPipeline>(File.ReadAllText(fileName));
            output.Name = Path.GetFileNameWithoutExtension(fileName).Replace(".dpl", "");
            output.FolderPath = Path.GetDirectoryName(fileName);
            return output;
        }
    }
}
