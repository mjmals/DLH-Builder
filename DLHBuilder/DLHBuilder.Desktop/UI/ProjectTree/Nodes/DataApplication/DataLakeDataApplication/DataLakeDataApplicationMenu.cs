using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataLakeDataApplicationMenu : ProjectTreeMenu
    {
        public DataLakeDataApplicationMenu(DataApplicationNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add ADLS Stage", AddDataStage));
        }

        DataApplicationNode Node
        {
            get => (DataApplicationNode)Tag;
            set => Tag = value;
        }

        void AddDataStage(object sender, EventArgs e)
        {
            Node.Application.Stages.Add(ADLSDataStage.New());
        }
    }    
}
