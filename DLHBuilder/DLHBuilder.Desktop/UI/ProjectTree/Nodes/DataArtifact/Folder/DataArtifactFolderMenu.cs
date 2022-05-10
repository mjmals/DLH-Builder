using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactFolderMenu : ProjectTreeMenu
    {
        public DataArtifactFolderMenu(ProjectTreeNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact", AddArtifact));
            Items.Add(new ProjectTreeMenuButton("Add Artifact Folder", AddArtifactFolder));
        }

        ProjectTreeNode Node
        {
            get => (ProjectTreeNode)Tag;
            set => Tag = value;
        }

        void AddArtifact(object sender, EventArgs e)
        {
            
        }

        void AddArtifactFolder(object sender, EventArgs e)
        {
            DataArtifactFolder folder = new DataArtifactFolder();
            folder.ID = Guid.NewGuid();
            folder.Name = "<New Folder>";
            folder.Path = new List<string>();
            
            if(Node.Tag.GetType() == typeof(DataArtifactFolder))
            {
                DataArtifactFolder parentFolder = (DataArtifactFolder)Node.Tag;

                switch(parentFolder.Path.Count > 0)
                {
                    case false:
                        folder.Path.Add(parentFolder.Name);
                        break;
                    default:
                        folder.Path.AddRange(parentFolder.FullPath.Split('.'));
                        break;
                }
            }
            else
            {
                folder.Path = new List<string>();
            }

            Node.Tree.Project.ArtifactFolders.Add(folder);

            DataArtifactFolderNode node = new DataArtifactFolderNode(folder);
            Node.Nodes.Add(node);
            Node.Tree.SelectedNode = node;
        }
    }
}
