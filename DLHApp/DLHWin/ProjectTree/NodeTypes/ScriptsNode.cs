using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHWin.Editors;

namespace DLHWin.ProjectTree.NodeTypes
{
    internal class ScriptsNode : ProjectTreeNode
    {
        public ScriptsNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            Text = "Scripts";
        }

        public ScriptsNode(ProjectDirectoryItem directoryItem, Type modelItemType) : base(directoryItem)
        {
            Text = "Scripts";
            ModelItemType = modelItemType;
        }

        Type ModelItemType { get; set; }

        protected override bool AllowRename => false;

        //protected override bool AllowChild => false;

        protected override string[]? Images => new string[] { "Script" };

        public override EditorCollection Editors()
        {
            string file = string.Empty;

            switch(DirectoryItem.Type)
            {
                case ProjectDirectoryItemType.File:
                    file = DirectoryItem.FullPath + DirectoryItem.Extension;
                    break;
                case ProjectDirectoryItemType.Folder:
                    file = Directory.GetFiles(DirectoryItem.FullPath).FirstOrDefault(x => Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(x)) == DirectoryItem.Name);
                    break;
            }

            if(!string.IsNullOrEmpty(file))
            {
                return new EditorCollection(new ScriptsEditor(file, ModelItemType));
            }

            return base.Editors();
        }
    }
}
