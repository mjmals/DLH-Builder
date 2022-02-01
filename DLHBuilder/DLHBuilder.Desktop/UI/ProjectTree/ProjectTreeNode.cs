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

        public virtual Control[] Editors()
        {
            return new Control[] { new PropertyEditor(Tag) };
        }

        public virtual string CollapsedImage { get; }

        public virtual string ExpandedImage { get; }

        public virtual bool AllowLabelChange { get => true; }

        public virtual void LabelChanged(string text)
        {
            Text = text;
        }
    }
}
