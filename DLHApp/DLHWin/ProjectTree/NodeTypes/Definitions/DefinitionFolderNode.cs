using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree.NodeTypes.Definitions
{
    internal class DefinitionFolderNode : ProjectTreeNode
    {
        public DefinitionFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Name == "Definitions")
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*.def.*").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
