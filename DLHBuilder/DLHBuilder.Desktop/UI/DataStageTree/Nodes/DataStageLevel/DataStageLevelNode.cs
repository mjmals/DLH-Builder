using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataStageLevelNode : DataStageTreeNode
    {
        public DataStageLevelNode(DataStageLevel level)
        {
            StageLevel = level;
            Text = level.Name;
            AddSubLevels(level.Levels);
            ContextMenuStrip = new DataStageLevelMenu(this);
        }

        public DataStageLevel StageLevel
        {
            get => (DataStageLevel)Tag;
            set => Tag = value;
        }

        void AddSubLevels(DataStageLevelCollection levels)
        {
            foreach(DataStageLevel level in levels)
            {
                DataStageLevelNode node = new DataStageLevelNode(level);
                Nodes.Add(node);
            }
        }
    }
}
