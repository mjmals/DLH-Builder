using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactsNode : ProjectTreeNode
    {
        public DataArtifactsNode(DataArtifactCollection artifacts)
        {
            Text = "Data Artifacts";
        }

        public override string CollapsedImage => "Folder Closed";

        public override string ExpandedImage => "Folder Open";

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactsMenu(this);
    }
}
