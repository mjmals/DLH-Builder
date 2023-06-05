using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStages
{
    public class DatabricksDataStage : DataStage, IModelItem
    {
        public string StorageFolderName { get; set; }

        public string HiveDatabaseName { get; set; }

        public override string OutputExtension => "dbxstg.json";

        public static DatabricksDataStage New(string parentPath, string name)
        {
            DatabricksDataStage stage = new DatabricksDataStage();
            stage.Name = name;
            stage.StorageFolderName = name;
            stage.HiveDatabaseName = name;
            stage.FolderPath = parentPath;
            stage.Templates = new TemplateReferenceCollection();
            stage.DefaultReferenceTemplates = new TemplateReferenceCollection();

            return stage;
        }
    }
}
