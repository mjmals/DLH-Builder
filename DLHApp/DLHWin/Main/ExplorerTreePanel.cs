using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.ProjectTree;

namespace DLHWin.Main
{
    internal class ExplorerTreePanel : Panel
    {
        public ExplorerTreePanel(ProjectController project)
        {
            Dock = DockStyle.Fill;
            Controls.Add(new Tree(project));
        }
    }
}
