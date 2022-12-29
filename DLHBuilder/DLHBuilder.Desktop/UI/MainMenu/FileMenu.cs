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
            DropDownItems.Add(NewMenu);
            NewMenu.DropDownItems.Add(NewProjectMenu);
            NewMenu.DropDownItems.Add(NewMSSQLProjectMenu);

            NewProjectMenu.Click += OnNewProjectMenuPressed;
            NewMSSQLProjectMenu.Click += OnNewMSSQLProjectMenuPressed;
        }

        public ToolStripMenuItem NewMenu = new ToolStripMenuItem() { Text = "New" };

        public ToolStripMenuItem NewProjectMenu = new ToolStripMenuItem() { Text = "New Project" };

        public ToolStripMenuItem NewMSSQLProjectMenu = new ToolStripMenuItem() { Text = "New MSSQL Project" };

        public EventHandler NewProjectMenuPressed;

        public EventHandler NewMSSQLProjectMenuPressed;

        void OnNewProjectMenuPressed(object sender, EventArgs e)
        {
            NewProjectMenuPressed.Invoke(NewProjectMenu, new NewProjectEventArgs(typeof(Project)));
        }

        void OnNewMSSQLProjectMenuPressed(object sender, EventArgs e)
        {
            NewMSSQLProjectMenuPressed.Invoke(NewMSSQLProjectMenu, new NewProjectEventArgs(typeof(MSSQLProject)));
        }
    }
}
