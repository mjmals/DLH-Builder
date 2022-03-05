using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactNode : ProjectTreeNode
    {
        public DataArtifactNode(DataArtifact artifact)
        {
            Artifact = artifact;

            Text = Artifact.Name;
        }

        public DataArtifact Artifact
        {
            get => (DataArtifact)Tag;
            set => Tag = value;
        }

        public override string CollapsedImage => "Data Artifact";

        public override string ExpandedImage => "Data Artifact";
    }
}
