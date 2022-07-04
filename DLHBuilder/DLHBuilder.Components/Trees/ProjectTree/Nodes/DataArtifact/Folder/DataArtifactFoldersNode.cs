using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactFoldersNode : ProjectTreeNode
    {
        public DataArtifactFoldersNode(DataArtifactFolderCollection folders, DataArtifactCollection artifacts)
        {
            Folders = folders;
            Artifacts = artifacts;
            Text = "Data Artifacts";
            Name = "Data Artifacts";
            AddFolders();
            AddArtifacts();
        }

        public DataArtifactFolderCollection Folders
        {
            get => (DataArtifactFolderCollection)Tag;
            set => Tag = value;
        }

        public DataArtifactCollection Artifacts { get; set; }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactFolderMenu(this);

        public override bool AllowLabelChange => false;

        void AddFolders()
        {
            foreach(DataArtifactFolder folder in Folders)
            {
                DataArtifactFolderNode node = new DataArtifactFolderNode(folder);

                ProjectTreeNode parentNode = folder.Path.Count == 0 ? this : (ProjectTreeNode)this.Nodes.Find("Data Artifacts." + string.Join('.', folder.Path), true).FirstOrDefault();
                parentNode.Nodes.Add(node);
            }
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in Artifacts)
            {
                DataArtifactNode node = new DataArtifactNode(artifact);

                ProjectTreeNode parentNode = artifact.ArtifactNamespace.Count == 0 ? this : (ProjectTreeNode)this.Nodes.Find("Data Artifacts." + string.Join('.', artifact.ArtifactNamespace), true).FirstOrDefault();
                parentNode.Nodes.Add(node);
            }
        }
    }
}
