using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataApplicationNode : ProjectTreeNode
    {
        public DataApplicationNode(DataApplication application)
        {

        }

        public DataApplication Application
        {
            get => (DataApplication)Tag;
            set => Tag = value;
        }
    }
}
