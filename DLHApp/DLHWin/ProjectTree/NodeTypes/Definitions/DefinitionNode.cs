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
    internal class DefinitionNode : ProjectTreeNode
    {
        public DefinitionNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override string[]? Images => new string[] { "Definition Set" };

        string DefinitionFile => Path.Combine(Environment.CurrentDirectory, DirectoryItem.FullPath + DirectoryItem.Extension);

        string ParentFolder => Path.GetDirectoryName(Path.GetDirectoryName(DirectoryItem.FullPath));

        public override EditorCollection Editors()
        {
            if (IsParentDataStructReference())
            {
                return DataStructReferenceEditors();
            }

            return new EditorCollection(new DefinitionEditor(DefinitionFile, null, null));
        }

        bool IsParentDataStructReference()
        {
            if (Directory.GetFiles(ParentFolder).Where(x => x.EndsWith(".ref.json")).Count() > 0)
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

                return new EditorCollection(new DefinitionEditor(DefinitionFile, dsr.Fields.Select(x => x.OutputName).ToArray(), "Output Name"));
            }

            return null;
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
