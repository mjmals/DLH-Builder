using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageLevelMenu : DataStageTreeMenu
    {
        public DataStageLevelMenu(DataStageLevelNode node)
        {
            Node = node;
            Items.Add(new DataStageTreeMenuButton("Add Sub Level", AddSubLevel));
            Items.Add(new DataStageTreeMenuButton("Delete Sub Level", DeleteSubLevel));
        }

        DataStageLevelNode Node
        {
            get => (DataStageLevelNode)Tag;
            set => Tag = value;
        }

        void AddSubLevel(object sender, EventArgs e)
        {
            ADLSDataStageLevel level = new ADLSDataStageLevel();
            level.Name = "<New Level>";

            Node.StageLevel.Levels.Add(level);

            DataStageLevelNode node = new DataStageLevelNode(level);
            Node.Nodes.Add(node);
            Node.TreeView.SelectedNode = node;
            Node.TreeView.SelectedNode.BeginEdit();
        }

        void DeleteSubLevel(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("This will delete this level and it's children.  Continue?", "Confirm Deletion", MessageBoxButtons.OKCancel);

            switch(response)
            {
                case DialogResult.OK:
                    DeleteStageLevel();
                    break;
                default:
                    break;
            }
        }

        void DeleteStageLevel()
        {
            DataStageLevelNode parent = (DataStageLevelNode)Node.Parent;
            parent.StageLevel.Levels.Remove(Node.StageLevel);
            Node.Parent.Nodes.Remove(Node);
        }
    }
}
