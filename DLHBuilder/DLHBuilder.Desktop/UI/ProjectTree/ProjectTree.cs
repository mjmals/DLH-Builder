using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ProjectTree : TreeView
    {
        public ProjectTree(Project project)
        {
            Project = project;

            LabelEdit = true;
            ImageList = Images.Items;
            AfterExpand += SetNodeImage;
            AfterCollapse += SetNodeImage;
            Nodes.Add(new ProjectNode(project));
            AfterLabelEdit += OnLabelEdit;
        }

        public Project Project 
        { 
            get => (Project)Tag; 
            set => Tag = value;
        }

        void SetNodeImage(object sender, TreeViewEventArgs e)
        {
            switch(e.Node.IsExpanded)
            {
                case true:
                    e.Node.ImageKey = ((ProjectTreeNode)e.Node).ExpandedImage;
                    break;
                case false:
                    e.Node.ImageKey = ((ProjectTreeNode)e.Node).CollapsedImage;
                    break;
            }

            e.Node.SelectedImageKey = e.Node.ImageKey;
        }

        void OnLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;

            switch (node.IsExpanded)
            {
                case true:
                    node.ImageKey = node.ExpandedImage;
                    node.SelectedImageKey = node.ExpandedImage;
                    break;
                default:
                    node.ImageKey = node.CollapsedImage;
                    node.SelectedImageKey = node.CollapsedImage;
                    break;
            }
        }
    }
}
