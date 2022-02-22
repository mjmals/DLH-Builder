using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataApplicationsNode : ProjectTreeNode
    {
        public DataApplicationsNode(DataApplicationCollection applications)
        {
            Applications = applications;
            Text = "Applications";
        }

        public DataApplicationCollection Applications
        {
            get => (DataApplicationCollection)Tag;
            set => Tag = value;
        }
    }
}
