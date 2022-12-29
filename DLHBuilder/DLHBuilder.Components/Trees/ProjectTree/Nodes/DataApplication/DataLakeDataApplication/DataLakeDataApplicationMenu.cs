using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Dialogs;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataLakeDataApplicationMenu : ProjectTreeMenu
    {
        public DataLakeDataApplicationMenu(DataApplicationNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add ADLS Stage", AddDataStage));
        }

        DataApplicationNode Node
        {
            get => (DataApplicationNode)Tag;
            set => Tag = value;
        }

        void AddDataStage(object sender, EventArgs e)
        {
            ADLSDataStageDialog dialog = new ADLSDataStageDialog(Node.Tree.Project);

            if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Node.Application.Stages.Add(dialog.DataStage);
            }
        }
    }    
}
