using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes.DataPipelines
{
    internal class DataPipelineNode : ProjectTreeNode
    {
        public DataPipelineNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(DataPipeline)));

            if(!Directory.Exists(Path.Combine(directoryItem.FullPath, "Tasks")))
            {
                Nodes.Add(new DataPipelineTaskFolderNode(new ProjectDirectoryItem() { Name = "Tasks", Parent = directoryItem.FullPath }));
            }
        }

        protected override string[]? Images => new string[] { "Data Pipeline" };

        public override EditorCollection Editors()
        {
            return new EditorCollection
            (
                new ModelItemObjectEditor(Path.Combine(DirectoryItem.FullPath, DirectoryItem.Name + ".dpl.json"), typeof(DataPipeline))
            );
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Pipelines"))
            {
                if (Directory.GetFiles(directoryItem.FullPath).Where(x => x.EndsWith(".dpl.json")).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
