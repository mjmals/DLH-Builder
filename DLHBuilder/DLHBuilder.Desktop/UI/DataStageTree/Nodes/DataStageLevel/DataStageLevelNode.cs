using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        public override void LabelUpdated(string text)
        {
            StageLevel.Name = text;
            base.LabelUpdated(text);
            SetParameters();
        }

        void SetParameters()
        {
            switch(ContainsParameter())
            {
                case true:
                    DataStageTree tree = (DataStageTree)TreeView;
                    tree.Stage.Parameters.Add(StageLevel.Name);
                    NodeFont = new Font(TreeView.Font, FontStyle.Bold);
                    break;
                default:
                    NodeFont = new Font(TreeView.Font, FontStyle.Regular);
                    break;
            }
        }

        bool ContainsParameter()
        {
            if(StageLevel.Name.Contains("$("))
            {
                return true;
            }

            return false;
        }
    }
}
