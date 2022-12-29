using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees
{
    public class ProjectTreeMenuButton : ToolStripMenuItem
    {
        public ProjectTreeMenuButton(string text, EventHandler task)
        {
            Text = text;
            Click += task;
        }
    }
}
