using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStageLevelMenu : DataStageTreeLevelMenu
    {
        public DataStageLevelMenu(DataStageLevelNode node)
        {
            Node = node;
            Items.Add(new DataStageTreeLevelMenuButton("Add Sub Level", AddSubLevel));
        }

        DataStageLevelNode Node
        {
            get => (DataStageLevelNode)Tag;
            set => Tag = value;
        }

        void AddSubLevel(object sender, EventArgs e)
        {
            DataStageLevel level = new DataStageLevel();
            level.Name = "<New Level>";

            Node.StageLevel.Levels.Add(level);

            DataStageLevelNode node = new DataStageLevelNode(level);
            Node.Nodes.Add(node);
        }
    }
}
