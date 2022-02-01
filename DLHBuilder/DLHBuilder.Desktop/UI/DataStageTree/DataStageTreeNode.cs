using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageTreeNode : TreeNode
    {
        public virtual void LabelUpdated(string text)
        {
            Text = text;
        }
    }
}
