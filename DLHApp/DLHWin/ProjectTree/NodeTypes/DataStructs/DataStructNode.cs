﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

namespace DLHWin.ProjectTree.NodeTypes.DataStructs
{
    internal class DataStructNode : ProjectTreeNode
    {
        public DataStructNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new DataStructFieldsFolderNode(directoryItem));
        }

        protected override bool AllowChild => false;

        protected override bool AllowDelete => true;

        protected override string[]? Images => new string[] { "Data Struct" };

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Data Structures"))
            {
                return true;
            }

            return false;
        }
    }
}