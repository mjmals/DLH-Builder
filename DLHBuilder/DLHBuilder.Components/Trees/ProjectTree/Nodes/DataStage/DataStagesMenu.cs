using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStagesMenu : ProjectTreeMenu
    {
        public DataStagesMenu(DataStagesNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add MSSQL Stage", AddDataStage));
        }

        DataStagesNode Node
        {
            get => (DataStagesNode)Tag;
            set => Tag = value;
        }     

        void AddDataStage(object sender, EventArgs e)
        {
            Node.Stages.Add(MSSQLDataStage.New());
        }
    }
}
