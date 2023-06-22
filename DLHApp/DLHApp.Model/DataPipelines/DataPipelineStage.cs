using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataPipelines
{
    public class DataPipelineStage : ModelItem, IModelItem
    {
        public override string OutputExtension => "dplstg.json";

        public List<string> IncludedItems { get; set; }

        public List<string> ExcludedItems { get; set; }

        public static DataPipelineStage New()
        {
            DataPipelineStage output = new DataPipelineStage();
            output.IncludedItems = new List<string>();
            output.ExcludedItems = new List<string>();
            output.Templates = new TemplateReferenceCollection();

            return output;
        }
    }
}
