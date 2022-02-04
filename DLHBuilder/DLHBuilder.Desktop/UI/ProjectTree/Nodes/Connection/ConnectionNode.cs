using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ConnectionNode : ProjectTreeNode
    {
        public ConnectionNode(DataConnection connection)
        {
            Connection = connection;

            Text = connection.Name;
        }

        public DataConnection Connection
        {
            get => (DataConnection)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Connection";

        public override string ExpandedImage => "Connection";
    }
}
