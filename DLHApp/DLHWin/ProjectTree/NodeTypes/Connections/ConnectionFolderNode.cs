using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors.Dialogs;
using DLHApp.Model.Connections;

namespace DLHWin.ProjectTree.NodeTypes.Connections
{
    internal class ConnectionFolderNode : ProjectTreeNode
    {
        public ConnectionFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowDelete => false;

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();

            ToolStripMenuItem addBtn = new ToolStripMenuItem() { Text = "Add Connection" };
            output.Items.Add(addBtn);

            ToolStripMenuItem addSqlBtn = new ToolStripMenuItem() { Text = "SQL Server" };
            addSqlBtn.Click += AddSqlConnection;
            addBtn.DropDownItems.Add(addSqlBtn);

            ToolStripMenuItem addWfsBtn = new ToolStripMenuItem() { Text = "Windows File Server" };
            addWfsBtn.Click += AddWfsConnection;
            addBtn.DropDownItems.Add(addWfsBtn);

            return output;
        }

        void AddSqlConnection(object sender, EventArgs e)
        {
            using (SqlConnectionDialog dialog = new SqlConnectionDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    SqlServerConnection connection = dialog.Connection;
                    connection.Save();
                    Tree.RefreshTree();
                }
            }
        }

        void AddWfsConnection(object sender, EventArgs e)
        {
            using (WindowsFileServerConnectionDialog dialog = new WindowsFileServerConnectionDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    WindowsFileServerConnection connection = dialog.Connection;
                    connection.Save();
                    Tree.RefreshTree();
                }
            }
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Name == "Connections")
            {
                return true;
            }

            return false;
        }
    }
}
