using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

namespace DLHWin.ProjectTree.NodeTypes.DataStructs
{
    internal class DataStructFieldsFolderNode : ProjectTreeNode
    {
        public DataStructFieldsFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Text = "Schema";
        }
    }
}
