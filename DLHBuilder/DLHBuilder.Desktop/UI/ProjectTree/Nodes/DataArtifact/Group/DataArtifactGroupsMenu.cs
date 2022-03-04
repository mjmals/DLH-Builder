using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactGroupsMenu : ProjectTreeMenu
    {
        public DataArtifactGroupsMenu(DataArtifactGroupsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Group", AddGroup));
        }

        DataArtifactGroupsNode Node
        {
            get => (DataArtifactGroupsNode)Tag;
            set => Tag = value;
        }

        void AddGroup(object sender, EventArgs e)
        {
            Node.Groups.Add(DataArtifactGroup.New());
        }
    }
}
