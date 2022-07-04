using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    public class ProjectTreeNode : TreeNode
    {
        public ProjectTreeNode()
        {
            ImageKey = CollapsedImage;
            SelectedImageKey = ExpandedImage;
        }

        public virtual EditorCollection Editors()
        {
            return new EditorCollection(new PropertyEditor(Tag));
        }

        public ProjectTree Tree
        {
            get
            {
                return (ProjectTree)TreeView;
            }
        }

        public virtual string CollapsedImage => "Folder Closed";

        public virtual string ExpandedImage => "Folder Open";

        public virtual bool AllowLabelChange { get => true; }

        public virtual void LabelChanged(string text)
        {
            Text = text;
        }
    }
}
