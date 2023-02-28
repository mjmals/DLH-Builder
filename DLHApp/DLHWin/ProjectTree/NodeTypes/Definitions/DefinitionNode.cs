using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.Definitions
{
    internal class DefinitionNode : ProjectTreeNode
    {
        public DefinitionNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override string[]? Images => new string[] { "Definition Set" };

        public override EditorCollection Editors()
        {
            return new EditorCollection(new TextFileEditor(Path.Combine(Environment.CurrentDirectory, DirectoryItem.FullPath + DirectoryItem.Extension)));
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.Extension.Contains(".def."))
            {
                return true;
            }

            return false;
        }
    }
}
