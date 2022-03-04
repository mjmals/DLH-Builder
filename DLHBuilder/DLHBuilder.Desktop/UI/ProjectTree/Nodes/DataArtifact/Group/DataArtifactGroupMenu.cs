using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactGroupMenu : ProjectTreeMenu
    {
        public DataArtifactGroupMenu(DataArtifactGroupNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact", AddArtifact));
        }

        DataArtifactGroupNode Node
        {
            get => (DataArtifactGroupNode)Tag;
            set => Tag = value;
        }

        void AddArtifact(object sender, EventArgs e)
        {
            Node.Group.Artifacts.Add(DataArtifact.New());
        }
    }
}
