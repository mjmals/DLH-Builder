using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataApplicationsMenu : ProjectTreeMenu
    {
        public DataApplicationsMenu(DataApplicationsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add SQL Application", AddSQLApplication));
            Items.Add(new ProjectTreeMenuButton("Add Data Lake Application", AddDataLakeApplication));
        }

        DataApplicationsNode Node
        {
            get => (DataApplicationsNode)Tag;
            set => Tag = value;
        }

        void AddSQLApplication(object sender, EventArgs e)
        {
            Node.Tree.Project.Applications.Add(SQLDataApplication.New());
        }

        void AddDataLakeApplication(object sender, EventArgs e)
        {
            Node.Tree.Project.Applications.Add(DataLakeDataApplication.New());
        }
    }
}
