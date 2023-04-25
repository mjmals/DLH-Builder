using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;
using DLHWin.Editors.Dialogs;

namespace DLHWin.ProjectTree.NodeTypes.DataStages
{
    internal class DataStageFolderNode : FolderNode
    {
        public DataStageFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = (ProjectTreeNodeMenu)base.Menu();

            output.InsertButton("Create Linked Data Struct Reference", LinkStructReference, 0);
            output.InsertButton("Create Unlinked Data Struct Reference", CreateStructReference, 1);

            return output;
        }

        void LinkStructReference(object sender, EventArgs e)
        {
            using (DataStructReferenceLinkDialog dialog = new DataStructReferenceLinkDialog(DirectoryItem.FullPath))
            {
                dialog.ShowDialog();

                if(dialog.DialogResult == DialogResult.OK)
                {
                    Tree.RefreshTree();
                }
            }
        }

        void CreateStructReference(object sender, EventArgs e)
        {
            DataStructReference dsr = DataStructReference.New();
            dsr.Name = "New Reference";
            dsr.FolderPath = this.Name;
            dsr.Save();

            Tree.RefreshTree();
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Applications") && directoryItem.FullPath.Contains("Stages") && directoryItem.Name != "Stages")
            {
                if (Directory.GetFiles(directoryItem.FullPath, "*stg.json").Count() == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
