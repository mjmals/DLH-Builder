using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStageFolderMenu : ProjectTreeMenu
    {
        public DataStageFolderMenu(DataStageFolderNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Folder", AddFolder));
            Items.Add(new ProjectTreeMenuButton("Delete Folder", DeleteFolder));
            Items.Add(new ProjectTreeMenuButton("Link Data Artifact", LinkArtifact));
        }

        public DataStageFolderNode Node
        {
            get => (DataStageFolderNode)Tag;
            set => Tag = value;
        }

        void AddFolder(object sender, EventArgs e)
        {
            DataStageFolder folder = new DataStageFolder();
            folder.ID = Guid.NewGuid();
            folder.Name = "<New Data Stage Folder>";
            folder.Path = new List<string>();
            folder.Path.AddRange(Node.Folder.Path);
            folder.Path.Add(Node.Folder.Name);

            Node.ParentStage.Folders.Add(folder);
        }

        void LinkArtifact(object sender, EventArgs e)
        {
            DataArtifactReferenceDialog refDialog = new DataArtifactReferenceDialog(Node.Tree.Project.Artifacts, Node.Folder.FullPath);

            if(refDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(DataArtifactReference reference in refDialog.SelectedReferences.Keys)
                {
                    Node.ParentStage.ArtifactReferences.Add(reference);
                }
            }
        }

        void DeleteFolder(object sender, EventArgs e)
        {
            Node.ParentStage.Folders.Remove(Node.Folder);
        }
    }
}
