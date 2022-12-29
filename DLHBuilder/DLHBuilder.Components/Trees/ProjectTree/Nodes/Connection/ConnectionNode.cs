using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionNode : ProjectTreeNode
    {
        public ConnectionNode(IDataConnection connection)
        {
            Connection = connection;
            Text = connection.Name;
            Nodes.Add(new ConnectionScriptsNode(connection.ScriptTemplates, connection));
        }

        public IDataConnection Connection
        {
            get => (IDataConnection)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Connection";

        public override string ExpandedImage => "Connection";

        public override ContextMenuStrip ContextMenuStrip => new ConnectionMenu(this);

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            Connection.Name = text;
        }
    }
}
