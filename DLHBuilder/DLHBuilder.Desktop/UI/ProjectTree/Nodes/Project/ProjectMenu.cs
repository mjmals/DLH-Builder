using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ProjectMenu : ProjectTreeMenu
    {
        public ProjectMenu(ProjectNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add SQL Application", AddSQLApplication));
        }

        ProjectNode Node
        {
            get => (ProjectNode)Tag;
            set => Tag = value;
        }

        void AddSQLApplication(object sender, EventArgs e)
        {
            Node.Project.Applications.Add(SQLDataApplication.New());
        }
    }
}
