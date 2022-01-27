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
            Tag = project;

            ImageList = Images.ImageList;
            Nodes.Add(new ProjectNode(project));
        }

        Project Project { get; set; }
    }
}
