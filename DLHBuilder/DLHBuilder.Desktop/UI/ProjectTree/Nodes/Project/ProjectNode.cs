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
            Tag = project;
            
            Text = project.Name;
            ImageKey = "Project";
            SelectedImageKey = ImageKey;
            
        }

        Project Project { get; set; }
    }
}
