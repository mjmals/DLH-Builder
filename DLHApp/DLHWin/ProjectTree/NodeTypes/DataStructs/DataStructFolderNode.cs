using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHWin.Editors.Dialogs;

namespace DLHWin.ProjectTree.NodeTypes.DataStructs
{
    internal class DataStructFolderNode : ProjectTreeNode
    {
        public DataStructFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {
            
        }

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output =  (ProjectTreeNodeMenu)base.Menu();

            output.InsertButton("Import Data Struct", ImportDataStruct, 0);

            return output;
        }

        void ImportDataStruct(object sender, EventArgs e)
        {
            using (DataStructImporterDialog dialog = new DataStructImporterDialog(DirectoryItem.FullPath))
            {
                dialog.ShowDialog();
            }

            Tree.RefreshTree();
        }

        protected override bool AllowDelete => DirectoryItem.Name == "Data Structures" ? false : true;

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if (directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Data Structures"))
            {
                return true;
            }

            return false;
        }
    }
}
