using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree.NodeTypes.BuildProfiles
{
    internal class BuildProfileLocalNode : ProjectTreeNode
    {
        public BuildProfileLocalNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        public override bool Ignore => true;

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Build Profiles") && directoryItem.Extension.Contains(".local."))
            {
                return true;
            }

            return false;
        }
    }
}
