﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Connections;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.Connections
{
    internal class SqlServerConnectionNode : ProjectTreeNode
    {
        public SqlServerConnectionNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(SqlServerConnection)));
        }

        protected override string[]? Images => new string[] { "Connection" };

        protected override bool AllowChild => false;

        protected override bool AllowDelete => true;

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.Parent.StartsWith(@"Connections\SQLServer"))
            {
                return true;
            }

            return false;
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ModelItemObjectEditor(this.Name + ".json", typeof(SqlServerConnection)));
        }
    }
}
