using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStageMenu : ProjectTreeMenu
    {
        public DataStageMenu(DataStageNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Folder", AddFolder));
        }

        DataStageNode Node
        {
            get => (DataStageNode)Tag;
            set => Tag = value;
        }

        void AddFolder(object sender, EventArgs e)
        {
            DataStageFolder folder = new DataStageFolder();
            folder.ID = Guid.NewGuid();
            folder.Name = "<New Data Stage Folder>";
            folder.Path = new List<string>();

            Node.Stage.Folders.Add(folder);
        }
    }
}
