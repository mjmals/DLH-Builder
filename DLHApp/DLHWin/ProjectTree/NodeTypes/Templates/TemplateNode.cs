using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.ProjectTree.NodeTypes.Templates
{
    internal class TemplateNode : ProjectTreeNode
    {
        public TemplateNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override string[]? Images => new string[] { "Template" };

        public override EditorCollection Editors()
        {
            return new EditorCollection(new TextFileEditor(DirectoryItem.FullPath + DirectoryItem.Extension));
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Templates"))
            {
                return true;
            }

            return false;
        }
    }
}
