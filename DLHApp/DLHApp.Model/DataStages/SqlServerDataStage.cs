using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStages
{
    public class SqlServerDataStage : DataStage, IModelItem
    {
        public string? SchemaName { get; set; }

        public override string OutputExtension => "sqlstg.json";

        public static SqlServerDataStage New(string parentPath, string name)
        {
            SqlServerDataStage stage = new SqlServerDataStage();
            stage.Name = name;
            stage.SchemaName = name;
            stage.FolderPath = parentPath;
            stage.Templates = new TemplateReferenceCollection();
            stage.DefaultReferenceTemplates = new TemplateReferenceCollection();

            return stage;
        }
    }
}
