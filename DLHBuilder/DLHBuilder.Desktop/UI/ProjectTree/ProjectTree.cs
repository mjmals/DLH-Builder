﻿using System;
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
            ImageList = Images.ImageList;
            AfterExpand += NodeExpanded;
            Nodes.Add(new ProjectNode(project));
            AfterLabelEdit += OnLabelEdit;
        }

        Project Project 
        { 
            get => (Project)Tag; 
            set => Tag = value;
        }

        void NodeExpanded(object sender, TreeViewEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;
            node.ImageKey = node.ExpandedImage;
            node.SelectedImageKey = node.ExpandedImage;
        }

        void NodeCollapsed(object sender, TreeViewEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;
            node.ImageKey = node.CollapsedImage;
            node.SelectedImageKey = node.CollapsedImage;
        }

        void OnLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;

            switch(node.AllowLabelChange)
            {
                case true:
                    node.LabelChanged(e.Label);
                    break;
                case false:
                    e.CancelEdit = true;
                    break;
            }
        }
    }
}