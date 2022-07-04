using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactsMenu : ProjectTreeMenu
    {
        public DataArtifactsMenu(DataArtifactsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Create Artifact from Connections", AddFromConnection));
        }

        DataArtifactsNode Node
        {
            get => (DataArtifactsNode)Tag;
            set => Tag = value;
        }

        void AddFromConnection(object sender, EventArgs e)
        {
            ConnectionSelectionDialog dialog = new ConnectionSelectionDialog(Node.Tree.Project.Connections);
            dialog.ShowDialog();

            if(dialog.DialogResult == DialogResult.OK)
            {
                if(dialog.SelectedConnection is SQLDataConnection)
                {
                    DataArtifactSQLImportDialog sqldialog = new DataArtifactSQLImportDialog((SQLDataConnection)dialog.SelectedConnection);
                    sqldialog.ShowDialog();
                }
            }
        }
    }
}
