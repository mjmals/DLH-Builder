using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.BuildProfiles;

namespace DLHWin.ProjectTree.NodeTypes.BuildProfiles
{
    internal class BuildProfileNode : ProjectTreeNode
    {
        public BuildProfileNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override string[]? Images => new string[] { "Build Profile" };

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ModelItemObjectEditor(this.Name, typeof(BuildProfile)));
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Build Profiles") && !directoryItem.FullPath.EndsWith(".local"))
            {
                return true;
            }

            return false;
        }
    }
}
