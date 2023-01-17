using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;
using DLHApp.Model.DataStructReferences;

namespace DLHWin.ProjectTree.NodeTypes.DataStructReferences
{
    internal class DataStructReferenceNode : ProjectTreeNode
    {
        public DataStructReferenceNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Nodes.Add(new ScriptsNode(directoryItem, typeof(DataStructReference)));
        }

        protected override string[]? Images => new string[] { "Data Struct" };

        public override EditorCollection Editors()
        {
            string file = Directory.GetFiles(DirectoryItem.FullPath).FirstOrDefault(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) == DirectoryItem.Name);

            if (!string.IsNullOrEmpty(file))
            {
                return new EditorCollection(new ModelItemObjectEditor(file, typeof(DataStructReference)));
            }

            return base.Editors();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Applications") && directoryItem.FullPath.Contains("Stages"))
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*ref.json").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
