using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ProjectNode : ProjectTreeNode
    {
        public ProjectNode(Project project)
        {
            Project = project;
            Text = project.Name;
            Nodes.Add(new DataStagesNode(project.Stages));

            Expand();
        }

        Project Project 
        { 
            get => (Project)Tag; 
            set => Tag = value;
        }

        public override string CollapsedImage => "Project";

        public override string ExpandedImage => "Project";
    }
}
