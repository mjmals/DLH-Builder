using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree.NodeTypes.DataApplications
{
    internal class DataApplicationFileNode : ProjectTreeNode
    {
        public DataApplicationFileNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        public override bool Ignore => true;

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Data Applications"))
            {
                if(directoryItem.Parent.EndsWith(Path.GetFileNameWithoutExtension(directoryItem.Name)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
