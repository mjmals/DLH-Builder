using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageTree : TreeView
    {
        public DataStageTree(DataStage stage)
        {
            Stage = stage;
            LabelEdit = true;
            AddLevelNodes(stage.Levels);
            AfterLabelEdit += LabelChanged;
        }

        DataStage Stage
        {
            get => (DataStage)Tag;
            set => Tag = value;
        }

        void AddLevelNodes(DataStageLevelCollection levels)
        {
            foreach(DataStageLevel level in levels)
            {
                DataStageLevelNode node = new DataStageLevelNode(level);
                Nodes.Add(node);
                SelectedNode = node;
            }
            
            ExpandAll();
        }

        void LabelChanged(object sender, NodeLabelEditEventArgs e)
        {
            DataStageTreeNode node = (DataStageTreeNode)e.Node;
            node.LabelUpdated(e.Label);
        }
    }
}
