using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactFolderMenu : ProjectTreeMenu
    {
        public DataArtifactFolderMenu(ProjectTreeNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Artifact", AddArtifact));
            Items.Add(new ProjectTreeMenuButton("Add Artifact Folder", AddArtifactFolder));
            Items.Add(new ProjectTreeMenuButton("Import from Connection", ImportArtifact));
        }

        ProjectTreeNode Node
        {
            get => (ProjectTreeNode)Tag;
            set => Tag = value;
        }

        void AddArtifact(object sender, EventArgs e)
        {
            DataArtifact artifact = new DataArtifact();
            artifact.ID = Guid.NewGuid();
            artifact.Name = "<New Data Artifact>";

            if (Node.Tag.GetType() == typeof(DataArtifactFolder))
            {
                DataArtifactFolder parentFolder = (DataArtifactFolder)Node.Tag;

                switch (parentFolder.Path.Count > 0)
                {
                    case false:
                        artifact.ArtifactNamespace.Add(parentFolder.Name);
                        break;
                    default:
                        artifact.ArtifactNamespace.AddRange(parentFolder.FullPath.Split('.'));
                        break;
                }
            }

            Node.Tree.Project.Artifacts.Add(artifact);
            DataArtifactNode node = new DataArtifactNode(artifact);
            Node.Nodes.Add(node);
            Node.Tree.SelectedNode = node;
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

        void ImportArtifact(object sender, EventArgs e)
        {
            ConnectionSelectionDialog connDialog = new ConnectionSelectionDialog(Node.Tree.Project.Connections);

            if(connDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if(connDialog.SelectedConnection.GetType() == typeof(SQLDataConnection))
            {
                SQLDataArtifactImportDialog importDialog = new SQLDataArtifactImportDialog((SQLDataConnection)connDialog.SelectedConnection, connDialog.Options);
                
                if (importDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadFromImportDialog(importDialog);
                }
            }

            
        }

        void LoadFromImportDialog(DataArtifactImportDialog importDialog)
        {
            foreach (DataArtifact artifact in importDialog.SelectedArtifacts.Keys)
            {
                artifact.ArtifactNamespace = new List<string>();

                if (Node.Tag.GetType() == typeof(DataArtifactFolder))
                {
                    artifact.ArtifactNamespace.AddRange(((DataArtifactFolder)Node.Tag).FullPath.Split('.'));
                }

                Node.Tree.Project.Artifacts.Add(artifact);

                DataArtifactNode node = new DataArtifactNode(artifact);
                Node.Nodes.Add(node);
                Node.Tree.SelectedNode = node;
            }
        }
    }
}
