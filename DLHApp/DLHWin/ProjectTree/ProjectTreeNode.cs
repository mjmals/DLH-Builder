using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;
using DLHWin.Editors;

namespace DLHWin.ProjectTree
{
    internal abstract class ProjectTreeNode : TreeNode
    {
        public ProjectTreeNode(ProjectDirectoryItem directoryItem)
        {
            Text = directoryItem.Name;
            Name = directoryItem.FullPath;
            ImageKey = Images[0];
            SelectedImageKey = Images[0];

            directoryItem.AllowChild = AllowChild;
        }

        public IModelItem ModelItem { get; set; }

        protected virtual string[]? Images => new string[] { "Folder Closed", "Folder Open" };

        protected virtual bool AllowChild => true;

        protected virtual bool AllowDelete => false;

        internal string GetNodeExpandedImage()
        {
            if (Images.Count() == 1)
            {
                return Images[0];
            }
            else
            {
                return Images[1];
            }
        }

        internal string GetNodeCollapsedImage()
        {
            return Images[0];
        }

        internal virtual bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            return false;
        }

        public override ContextMenuStrip ContextMenuStrip => Menu();

        protected virtual ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();
            
            if(AllowDelete)
            {
                output.AddButton("Delete", DeleteNode);
            }

            return output;
        }

        public virtual EditorCollection Editors()
        {
            return null;
        }

        void DeleteNode(object sender, EventArgs e)
        {

        }
    }
}
