using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataApplications;

namespace DLHWin.ProjectTree.NodeTypes.DataApplications
{
    internal class DataApplicationNode : ProjectTreeNode
    {
        public DataApplicationNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override string[]? Images => new string[] { "Data Application" };

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ModelItemObjectEditor(ApplicationFilePath(), typeof(DataApplication)));
        }

        string ApplicationFilePath()
        {
            return Directory.GetFiles(this.Name, "*app.json")[0];
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.Parent == "Data Applications")
            {
                if(Directory.GetFiles(directoryItem.FullPath, "*app.json").Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
