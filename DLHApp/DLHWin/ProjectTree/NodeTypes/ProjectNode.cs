using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;

namespace DLHWin.ProjectTree.NodeTypes
{
    internal class ProjectNode : ProjectTreeNode
    {
        public ProjectNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            
        }

        public ProjectNode(ProjectDirectoryItem directoryItem, ProjectController project) : base(directoryItem)
        {
            Project = project;
        }

        public ProjectController Project { get; set; }

        protected override bool AllowRename => false;

        protected override string[]? Images => new string[] { "Project" };
    }
}
