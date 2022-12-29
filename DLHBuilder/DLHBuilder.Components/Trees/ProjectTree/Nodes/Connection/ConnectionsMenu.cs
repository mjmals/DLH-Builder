using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionsMenu : ProjectTreeMenu
    {
        public ConnectionsMenu(ConnectionsNode node)
        {
            Node = node;

            Items.Add(new ProjectTreeMenuButton("Add SQL Connection", AddSQLConnection));
            Items.Add(new ProjectTreeMenuButton("Add Azure Blob Storage Connection", AddAzureStorageConnection));
        }

        ConnectionsNode Node
        {
            get => (ConnectionsNode)Tag;
            set => Tag = value;
        }

        void AddSQLConnection(object sender, EventArgs e)
        {
            SQLConnectionDialog dialog = new SQLConnectionDialog();

            if(dialog.DialogResult == DialogResult.OK)
            {
                ConnectionNode node = new ConnectionNode(dialog.Connection);

                Node.Nodes.Add(node);
                Node.Connections.Add(dialog.Connection);
                Node.TreeView.SelectedNode = node;
            }
        }

        void AddAzureStorageConnection(object sender, EventArgs e)
        {
            AzureStorageConnectionDialog dialog = new AzureStorageConnectionDialog();

            if (dialog.DialogResult == DialogResult.OK)
            {
                ConnectionNode node = new ConnectionNode(dialog.Connection);

                Node.Nodes.Add(node);
                Node.Connections.Add(dialog.Connection);
                Node.TreeView.SelectedNode = node;
            }
        }
    }
}
