using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageMenu : ProjectTreeMenu
    {
        public DataStageMenu(DataStageNode node)
        {
            Node = node;
        }

        DataStageNode Node
        {
            get => (DataStageNode)Tag;
            set => Tag = value;
        }

        void ImportArtifact(object sender, EventArgs e)
        {
            DataArtifactImportDialog importdialog;

            if(Node.Stage.GetType() == typeof(MSSQLDataStage))
            {
                SQLConnectionSelectionDialog conndialog = new SQLConnectionSelectionDialog(Node.Tree.Project.Connections);
                
                if(conndialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                importdialog = new SQLDataArtifactImportDialog((SQLDataConnection)conndialog.SelectedConnection);

                if(importdialog.ShowDialog() == DialogResult.OK)
                {
                    foreach(DataArtifact artifact in importdialog.SelectedArtifacts.Keys)
                    {
                        Node.Stage.Artifacts.Add(artifact);
                    }
                }
            }
        }
    }
}
