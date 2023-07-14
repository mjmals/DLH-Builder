using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.DataPipelines
{
    internal class DataPipelineTaskNode : ProjectTreeNode
    {
        public DataPipelineTaskNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(DataPipelineTask)));
        }

        protected override string[]? Images => new string[] { "Data Pipeline Task" };

        protected override bool AllowChild => false;

        protected override bool AllowDelete => true;

        public override EditorCollection Editors()
        {
            return new EditorCollection
            (
                new ModelItemObjectEditor(DirectoryItem.FullPath + DirectoryItem.Extension, typeof(DataPipelineTask))
            );
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.Extension == ".dpltsk.json")
            {
                return true;
            }

            return false;
        }
    }
}
