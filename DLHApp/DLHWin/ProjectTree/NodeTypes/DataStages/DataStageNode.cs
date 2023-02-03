using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStages;

namespace DLHWin.ProjectTree.NodeTypes.DataStages
{
    internal class DataStageNode : ProjectTreeNode
    {
        public DataStageNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(DataStage)));
        }

        protected override string[]? Images => new string[] { "Data Stage" };

        public override EditorCollection Editors()
        {
            string file = Directory.GetFiles(DirectoryItem.FullPath).FirstOrDefault(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) == DirectoryItem.Name);

            if(!string.IsNullOrEmpty(file))
            {
                return new EditorCollection(new ModelItemObjectEditor(file, typeof(DataStage)));
            }

            return base.Editors();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Applications") && directoryItem.FullPath.Contains("Stages"))
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*stg.json").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
