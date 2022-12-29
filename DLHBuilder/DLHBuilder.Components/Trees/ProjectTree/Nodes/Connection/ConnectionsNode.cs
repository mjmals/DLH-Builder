using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionsNode : ProjectTreeNode
    {
        public ConnectionsNode(DataConnectionCollection connections)
        {
            Text = "Connections";
            Connections = connections;
            AddConnections();
        }

        public DataConnectionCollection Connections
        {
            get => (DataConnectionCollection)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";

        public override ContextMenuStrip ContextMenuStrip => new ConnectionsMenu(this);

        void AddConnections()
        { 
            foreach(DataConnection connection in Connections.OrderBy(x => x.Name))
            {
                Nodes.Add(new ConnectionNode(connection));
            }
        }
    }
}
