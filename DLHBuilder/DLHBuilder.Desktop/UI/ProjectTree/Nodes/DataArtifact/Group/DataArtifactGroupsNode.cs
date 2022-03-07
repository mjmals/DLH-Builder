using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactGroupsNode : ProjectTreeNode
    {
        public DataArtifactGroupsNode(DataArtifactGroupCollection groups)
        {
            Text = "Data Artifacts";
            Groups = groups;
            AddGroups();
        }

        public DataArtifactGroupCollection Groups
        {
            get => (DataArtifactGroupCollection)Tag;
            set
            {
                value.GroupAdded += OnGroupAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactGroupsMenu(this);

        void OnGroupAdded(object sender, DataArtifactGroupEventArgs e)
        {
            DataArtifactGroupNode node = AddGroupNode(e.Group);
            Tree.SelectedNode = node;
            Expand();
        }

        void AddGroups()
        {
            foreach (DataArtifactGroup group in Groups)
            {
                AddGroupNode(group);
            }
        }

        DataArtifactGroupNode AddGroupNode(DataArtifactGroup group)
        {
            DataArtifactGroupNode node = new DataArtifactGroupNode(group);
            Nodes.Add(node);
            return node;
        }
    }
}
