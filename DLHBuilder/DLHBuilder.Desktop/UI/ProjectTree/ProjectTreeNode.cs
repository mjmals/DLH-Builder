using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class ProjectTreeNode : TreeNode
    {
        public ProjectTreeNode()
        {
            
        }

        public virtual bool ShowPropertyEditor { get => true; }

        public virtual Type[] Editors { get; set; }
    }
}
