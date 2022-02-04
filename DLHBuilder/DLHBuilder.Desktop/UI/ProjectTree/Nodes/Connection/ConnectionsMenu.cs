using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ConnectionsMenu : ProjectTreeMenu
    {
        public ConnectionsMenu(ConnectionsNode node)
        {
            Node = node;

            Items.Add(new ProjectTreeMenuButton("Add SQL Connection", AddSQLConnection));
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
    }
}
