using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.BuildProfiles;

namespace DLHWin.ProjectTree.NodeTypes.BuildProfiles
{
    internal class BuildProfileFolderNode : FolderNode
    {
        public BuildProfileFolderNode(ProjectDirectoryItem directoryItem) : base(directoryItem)
        {

        }

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = (ProjectTreeNodeMenu)base.Menu();

            output.InsertButton("Add Build Profile", AddProfile, 0);

            return output;
        }

        void AddProfile(object sender, EventArgs e)
        {
            BuildProfile profile = BuildProfile.New();
            profile.Name = "New Build Profile";
            profile.FolderPath = this.Name.Split(@"\").Count() > 1 ? this.Name : String.Empty;
            profile.Save();

            ProjectDirectoryItem dirItem = new ProjectDirectoryItem();
            dirItem.Name = profile.Name;
            dirItem.Parent = this.Name;
            dirItem.Type = ProjectDirectoryItemType.File;
            dirItem.Extension = ".json";

            Tree.AddNode(dirItem);
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.Folder && directoryItem.FullPath.StartsWith("Build Profiles"))
            {
                return true;
            }

            return false;
        }
    }
}
