using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactFoldersNode : ProjectTreeNode
    {
        public DataArtifactFoldersNode(DataArtifactFolderCollection folders)
        {
            Folders = folders;
            Text = "Data Artifacts";
            Name = "Data Artifacts";
            AddFolders();
        }

        public DataArtifactFolderCollection Folders
        {
            get => (DataArtifactFolderCollection)Tag;
            set => Tag = value;
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactFolderMenu(this);

        void AddFolders()
        {
            foreach(DataArtifactFolder folder in Folders)
            {
                DataArtifactFolderNode node = new DataArtifactFolderNode(folder);

                ProjectTreeNode parentNode = folder.Path.Count == 0 ? this : (ProjectTreeNode)this.Nodes.Find("Data Artifacts." + string.Join('.', folder.Path), true).FirstOrDefault();
                parentNode.Nodes.Add(node);
            }
        }
    }
}
