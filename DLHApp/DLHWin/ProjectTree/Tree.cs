using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Styles;
using System.Reflection;
using DLHWin.ProjectTree.NodeTypes;

namespace DLHWin.ProjectTree
{
    internal class Tree : TreeView
    {
        public Tree(ProjectController project)
        {
            Dock = DockStyle.Fill;
            AfterExpand += SetNodeImage;
            AfterCollapse += SetNodeImage;
            ImageList = Images.List;
            Project = project;
        }

        public ProjectController Project 
        { 
            get => _project; 
            set
            {
                _project = value;

                if (Project != null)
                {
                    LoadProjectNodes();
                }
            }
        }

        private ProjectController _project { get; set; }

        void SetNodeImage(object sender, TreeViewEventArgs e)
        {
            ProjectTreeNode node = (ProjectTreeNode)e.Node;

            switch(node.IsExpanded && node.Nodes.Count > 0)
            {
                case true:
                    node.ImageKey = node.GetNodeExpandedImage();
                    node.SelectedImageKey = node.GetNodeExpandedImage();
                    break;
                default:
                    node.ImageKey = node.GetNodeCollapsedImage();
                    node.SelectedImageKey = node.GetNodeCollapsedImage();
                    break;
            }
        }

        void LoadProjectNodes()
        {
            Nodes.Clear();

            ProjectNode projectNode = new ProjectNode(new ProjectDirectoryItem() { Name = Project.Name }, Project);
            Nodes.Add(projectNode);

            foreach (ProjectDirectoryItem directoryItem in Project.Directory)
            {
                AddNode(directoryItem);
            }

            projectNode.Expand();
        }

        public void AddNode(ProjectDirectoryItem directoryItem)
        {
            ProjectTreeNode node = NewNode(directoryItem);

            if(node.Ignore)
            {
                return;
            }

            if (string.IsNullOrEmpty(directoryItem.Parent))
            {
                Nodes[0].Nodes.Add(node);
            }
            else
            {
                ProjectTreeNode parentNode = (ProjectTreeNode)this.Nodes.Find(directoryItem.Parent, true).FirstOrDefault();

                if (parentNode.DirectoryItem.AllowChild)
                {
                    parentNode.Nodes.Add(node);
                }
            }
        }

        ProjectTreeNode NewNode(ProjectDirectoryItem directoryItem)
        {
            Type[] nodeTypes = this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(ProjectTreeNode)) && x.IsAbstract == false).ToArray();

            foreach(Type nodeType in nodeTypes)
            {
                ProjectTreeNode node = (ProjectTreeNode)Activator.CreateInstance(nodeType, directoryItem);

                if(node.ValidateType(directoryItem))
                {
                    return node;
                }
            }

            return new FolderNode(directoryItem);
        }

        public void RefreshTree()
        {
            ProjectDirectory refreshDir = new ProjectDirectory(Environment.CurrentDirectory);

            foreach(ProjectDirectoryItem refreshItem in refreshDir)
            {
                if(!Project.Directory.Exists(x => x.FullPath == refreshItem.FullPath))
                {
                    AddNode(refreshItem);
                    Project.Directory.Add(refreshItem);
                }
            }

            foreach(ProjectDirectoryItem existingItem in Project.Directory.ToList())
            {
                if(!refreshDir.Exists(x => x.FullPath == existingItem.FullPath))
                {
                    Project.Directory.Remove(existingItem);
                    Nodes.RemoveByKey(existingItem.Name);
                }
            }
        }
    }
}
