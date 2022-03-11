using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactFolderMenu : ProjectTreeMenu
    {
        public DataArtifactFolderMenu(DataArtifactFolderNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact", AddArtifact));
            Items.Add(new ProjectTreeMenuButton("Add Artifact Folder", AddArtifactFolder));
        }

        DataArtifactFolderNode Node
        {
            get => (DataArtifactFolderNode)Tag;
            set => Tag = value;
        }

        void AddArtifact(object sender, EventArgs e)
        {
            string path = Node.FolderPath();
            Node.ParentStage.Stage.Artifacts.Add(DataArtifact.New(Node.FolderPath()));
        }

        void AddArtifactFolder(object sender, EventArgs e)
        {
            DataArtifactFolderNode node = new DataArtifactFolderNode("<New Folder>", Node, Node.ParentStage);
            Node.Nodes.Add(node);
            Node.Tree.SelectedNode = node;
        }
    }
}
