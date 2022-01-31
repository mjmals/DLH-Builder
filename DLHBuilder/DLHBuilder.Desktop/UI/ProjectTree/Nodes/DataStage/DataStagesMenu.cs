using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStagesMenu : ProjectTreeMenu
    {
        public DataStagesMenu(DataStagesNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("New Data Stage", AddDataStage));
        }

        DataStagesNode Node
        {
            get => (DataStagesNode)Tag;
            set => Tag = value;
        }     

        void AddDataStage(object sender, EventArgs e)
        {
            DataStage stage = new DataStage();
            stage.Name = "<New Data Stage>";

            DataStageNode stagenode = new DataStageNode(stage);

            Node.Nodes.Add(stagenode);
            Node.TreeView.SelectedNode = stagenode;
            Node.Expand();
        }
    }
}
