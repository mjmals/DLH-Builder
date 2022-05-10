using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageMenu : ProjectTreeMenu
    {
        public DataStageMenu(DataStageNode node)
        {
            Node = node;
        }

        DataStageNode Node
        {
            get => (DataStageNode)Tag;
            set => Tag = value;
        }
    }
}
