using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.LoadSteps
{
    internal class LoadStepNode : ProjectTreeNode
    {
        public LoadStepNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override bool AllowChild => false;

        protected override bool AllowDelete => true;

        protected override string[]? Images => new string[] { "Load Step" };

        public override EditorCollection Editors()
        {
            return new EditorCollection(new TextFileEditor(Path.Combine(Environment.CurrentDirectory, DirectoryItem.FullPath + DirectoryItem.Extension)));
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.Extension.Contains("loadstep"))
            {
                return true;
            }

            return false;
        }
    }
}
