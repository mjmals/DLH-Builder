using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactsNode : ProjectTreeNode
    {
        public DataArtifactsNode()
        {
            Text = "Data Artifacts";
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";
    }
}
