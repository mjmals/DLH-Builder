using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataApplications;

namespace DLHWin.ProjectTree.NodeTypes.DataApplications
{
    internal class DataApplicationFolderNode : ProjectTreeNode
    {
        public DataApplicationFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowDelete => false;

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();

            ToolStripMenuItem addBtn = new ToolStripMenuItem() { Text = "Add Data Application" };
            output.Items.Add(addBtn);

            ToolStripMenuItem addSqlBtn = new ToolStripMenuItem() { Text = "SQL Server" };
            addSqlBtn.Click += AddSqlApplication;
            addBtn.DropDownItems.Add(addSqlBtn);

            ToolStripMenuItem addDatabricksBtn = new ToolStripMenuItem() { Text = "Databricks" };
            addDatabricksBtn.Click += AddDatabricksApplication;
            addBtn.DropDownItems.Add(addDatabricksBtn);

            return output;
        }

        void AddSqlApplication(object sender, EventArgs e)
        {
            SqlServerDataApplication app = SqlServerDataApplication.New();
            app.Save();
            Tree.RefreshTree();
        }

        void AddDatabricksApplication(object sender, EventArgs e)
        {
            DatabricksDataApplication app = DatabricksDataApplication.New();
            app.Save();
            Tree.RefreshTree();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Name == "Data Applications")
            {
                return true;
            }

            return false;
        }
    }
}
