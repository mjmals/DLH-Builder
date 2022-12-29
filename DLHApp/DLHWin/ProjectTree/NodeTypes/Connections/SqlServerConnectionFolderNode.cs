using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

namespace DLHWin.ProjectTree.NodeTypes.Connections
{
    internal class SqlServerConnectionFolderNode : ProjectTreeNode
    {
        public SqlServerConnectionFolderNode(ProjectDirectoryItem projectDirectory) : base(projectDirectory)
        {

        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath == @"Connections\SQLServer")
            {
                return true;
            }

            return false;
        }
    }
}
