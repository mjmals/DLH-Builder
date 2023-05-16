using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataApplications;
using DLHApp.Model.DataStages;

namespace DLHWin.ProjectTree.NodeTypes.DataStages
{
    internal class DataStageRootFolderNode : ProjectTreeNode
    {
        public DataStageRootFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowDelete => false;

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();

            if(ParentApplication == null)
            {
                ParentApplication = GetParentApplication();
            }

            if(ParentApplication == null)
            {
                return output;
            }

            if (ParentApplication is SqlServerDataApplication)
            {
                output.AddButton("Add SQL Server Data Stage", AddSqlStage);
            }

            if(ParentApplication is DatabricksDataApplication)
            {
                output.AddButton("Add Databricks Data Stage", AddDatabricksStage);
            }

            return output;
        }

        DataApplication ParentApplication { get; set; }

        DataApplication GetParentApplication()
        {
            foreach(string file in Directory.GetFiles(DirectoryItem.Parent, "*app.json"))
            {
                return DataApplication.Load(file);
            }

            return null;
        }

        void AddSqlStage(object sender, EventArgs e)
        {
            SqlServerDataStage stage = SqlServerDataStage.New(DirectoryItem.Parent + @"\Stages", "New SQL Data Stage");
            stage.Save();
            Tree.RefreshTree();
        }

        void AddDatabricksStage(object sender, EventArgs e)
        {
            DatabricksDataStage stage = DatabricksDataStage.New(DirectoryItem.Parent + @"\Stages", "New Databricks Data Stage");
            stage.Save();
            Tree.RefreshTree();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Applications") && directoryItem.Name == "Stages")
            {
                return true;
            }
            
            return false;
        }
    }
}
