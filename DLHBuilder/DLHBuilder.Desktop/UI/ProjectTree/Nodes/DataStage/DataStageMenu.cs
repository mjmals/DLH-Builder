using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStageMenu : ProjectTreeMenu
    {
        public DataStageMenu(DataStageNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact", AddArtifact));
        }

        DataStageNode Node
        {
            get => (DataStageNode)Tag;
            set => Tag = value;
        }

        void AddArtifact(object sender, EventArgs e)
        {
            Node.Stage.Artifacts.Add(DataArtifact.New());
        }
    }
}
