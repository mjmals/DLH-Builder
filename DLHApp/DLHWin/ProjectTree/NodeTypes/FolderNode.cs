using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

namespace DLHWin.ProjectTree.NodeTypes
{
    internal class FolderNode : ProjectTreeNode
    {
        public FolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowDelete => true;
    }
}
