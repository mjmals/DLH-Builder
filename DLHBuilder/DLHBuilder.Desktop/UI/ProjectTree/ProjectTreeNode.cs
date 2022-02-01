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
            ImageKey = CollapsedImage;
            SelectedImageKey = ExpandedImage;
        }

        public virtual bool ShowPropertyEditor { get => true; }

        public virtual Control[] Editors()
        {
            return new Control[] { new PropertyEditor(Tag) };
        }

        public virtual string CollapsedImage { get; }

        public virtual string ExpandedImage { get; }
    }
}
