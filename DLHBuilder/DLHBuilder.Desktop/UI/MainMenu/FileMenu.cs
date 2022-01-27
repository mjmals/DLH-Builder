using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class FileMenu : ToolStripMenuItem
    {
        public FileMenu()
        {
            Text = "File";
            DropDownItems.Add(NewProjectMenu);

            NewProjectMenu.Click += OnNewProjectMenuPressed;
        }

        public ToolStripMenuItem NewProjectMenu = new ToolStripMenuItem() { Text = "New Project" };

        public EventHandler NewProjectMenuPressed;

        void OnNewProjectMenuPressed(object sender, EventArgs e)
        {
            NewProjectMenuPressed.Invoke(this, null);
        }
    }
}
