using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.Keys;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.Keys
{
    internal class KeyNode : ProjectTreeNode
    {
        public KeyNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            
        }

        protected override bool AllowChild => false;

        protected override bool AllowDelete => true;

        protected override string[]? Images => new string[] { "Key" };

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Keys"))
            {
                return true;
            }

            return false;
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ModelItemObjectEditor(this.Name + ".json", typeof(Key)));
        }
    }
}
