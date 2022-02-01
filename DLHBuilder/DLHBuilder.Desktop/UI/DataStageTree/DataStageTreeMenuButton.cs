using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageTreeMenuButton : ToolStripMenuItem
    {
        public DataStageTreeMenuButton(string text, EventHandler task)
        {
            Text = text;
            Click += task;
        }
    }
}
