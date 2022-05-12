﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStageFolderMenu : ProjectTreeMenu
    {
        public DataStageFolderMenu(DataStageFolderNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Folder", AddFolder));
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
            DataArtifactReferenceDialog refDialog = new DataArtifactReferenceDialog(Node.Tree.Project.Artifacts);

            if(refDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(DataArtifactReference reference in refDialog.SelectedReferences.Keys)
                {
                    Node.ParentStage.ArtifactReferences.Add(reference);

                    DataArtifactReferenceNode node = new DataArtifactReferenceNode(reference);
                    Node.Nodes.Add(node);
                    Node.Tree.SelectedNode = node;
                }
            }
        }
    }
}
