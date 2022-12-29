using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree
{
    internal class ProjectTreeNodeMenu : ContextMenuStrip
    {
        public void AddButton(string title, EventHandler task)
        {
            Items.Add(title, null, task);
        }
    }
}
