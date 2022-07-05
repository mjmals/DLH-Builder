using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceMenu : ProjectTreeMenu
    {
        public DataArtifactReferenceMenu(DataArtifactReferenceNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Delete Artifact Reference", DeleteReference));
        }

        DataArtifactReferenceNode Node
        {
            get => (DataArtifactReferenceNode)Tag;
            set => Tag = value;
        }

        void DeleteReference(object sender, EventArgs e)
        {
            Node.Stage.ArtifactReferences.Remove(Node.Reference);
        }
    }
}
