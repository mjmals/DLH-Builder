using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class SQLDataApplicationMenu : ProjectTreeMenu
    {
        public SQLDataApplicationMenu(DataApplicationNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add MSSQL Stage", AddDataStage));
        }

        DataApplicationNode Node
        {
            get => (DataApplicationNode)Tag;
            set => Tag = value;
        }


        void AddDataStage(object sender, EventArgs e)
        {
            Node.Application.Stages.Add(MSSQLDataStage.New());
        }
    }
}
