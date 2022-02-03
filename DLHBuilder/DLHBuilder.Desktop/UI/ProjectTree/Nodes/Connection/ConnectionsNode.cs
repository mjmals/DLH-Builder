using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ConnectionsNode : ProjectTreeNode
    {
        public ConnectionsNode()
        {
            Text = "Connections";
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";
    }
}
