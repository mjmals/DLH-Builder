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
            Items.Add(new ProjectTreeMenuButton("Add Artifact Folder", AddArtifactFolder));
            Items.Add(new ProjectTreeMenuButton("Import from Connection", ImportArtifact));
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

        void AddArtifactFolder(object sender, EventArgs e)
        {
            DataArtifactFolderNode node = new DataArtifactFolderNode("<New Folder>", null, Node);
            Node.Nodes.Add(node);
            Node.Tree.SelectedNode = node;
        }

        void ImportArtifact(object sender, EventArgs e)
        {
            DataArtifactImportDialog importdialog;

            if(Node.Stage.GetType() == typeof(MSSQLDataStage))
            {
                SQLConnectionSelectionDialog conndialog = new SQLConnectionSelectionDialog(Node.Tree.Project.Connections);
                conndialog.ShowDialog();

                if(conndialog.DialogResult != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                importdialog = new SQLDataArtifactImportDialog((SQLDataConnection)conndialog.SelectedConnection);
                importdialog.ShowDialog();
            }
        }
    }
}
