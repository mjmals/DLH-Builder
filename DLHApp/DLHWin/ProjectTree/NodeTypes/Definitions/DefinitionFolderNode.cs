using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors;
using DLHWin.Editors.DefinitionEditors;
using DLHApp.Model.DataStructReferences;

namespace DLHWin.ProjectTree.NodeTypes.Definitions
{
    internal class DefinitionFolderNode : ProjectTreeNode
    {
        public DefinitionFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            
        }

        string ParentFolder => DirectoryItem.Parent;

        public override EditorCollection Editors()
        {
            if(IsParentDataStructReference())
            {
                return DataStructReferenceEditors();
            }

            return base.Editors();
        }

        bool IsParentDataStructReference()
        {
            if(Directory.GetFiles(DirectoryItem.Parent).Where(x => x.EndsWith(".ref.json")).Count() > 0)
            {
                return true;
            }

            return false;
        }

        EditorCollection DataStructReferenceEditors()
        {
            string refFile = Directory.GetFiles(ParentFolder).FirstOrDefault(x => x.EndsWith(".ref.json"));

            if (!string.IsNullOrEmpty(refFile))
            {
                DataStructReference dsr = DataStructReference.Load(refFile);

                return new EditorCollection(new DefinitionGroupEditor(DirectoryItem.FullPath, dsr.Fields.Select(x => x.OutputName).ToArray(), "Output Name"));
            }

            return null;
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Name == "Definitions")
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*.def.*").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
