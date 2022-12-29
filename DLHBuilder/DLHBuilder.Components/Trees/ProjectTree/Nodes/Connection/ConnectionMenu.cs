using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionMenu : ProjectTreeMenu
    {
        public ConnectionMenu(ConnectionNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Delete Connection", DeleteConnection));
        }

        ConnectionNode Node
        {
            get => (ConnectionNode)Tag;
            set => Tag = value;
        }

        void DeleteConnection(object sender, EventArgs e)
        {
            Node.Tree.Project.Connections.Remove(Node.Connection);
        }
    }
}
