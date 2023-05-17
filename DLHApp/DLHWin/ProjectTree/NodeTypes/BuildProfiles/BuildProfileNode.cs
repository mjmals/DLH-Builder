using DLHWin.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.BuildProfiles;
using DLHWin.Editors.Dialogs;

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

        protected override ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();

            output.AddButton("Run Build Profile", Build);
            output.AddButton("Delete", DeleteProfile);

            return output;
        }

        void Build(object sender, EventArgs e)
        {
            using (BuildProfileRunDialog dialog = new BuildProfileRunDialog(DirectoryItem.FullPath.Substring(DirectoryItem.FullPath.IndexOf("Build Profiles"))))
            {
                dialog.ShowDialog();
            }
        }

        void DeleteProfile(object sender, EventArgs e)
        {
            File.Delete(DirectoryItem.FullPath + DirectoryItem.Extension);
            File.Delete(DirectoryItem.FullPath + ".local" + DirectoryItem.Extension);

            if(Tree.Project.Directory.Contains(DirectoryItem))
            {
                Tree.Project.Directory.Remove(DirectoryItem);
            }

            if(Tree.Project.Directory.Exists(x => x.FullPath == DirectoryItem.FullPath + ".local"))
            {
                Tree.Project.Directory.Remove(Tree.Project.Directory.First(x => x.FullPath == DirectoryItem.FullPath + ".local"));
            }

            Tree.DeleteNode(DirectoryItem);
        }

        internal override bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            if(directoryItem.Type == ProjectDirectoryItemType.File && directoryItem.FullPath.StartsWith("Build Profiles") && !directoryItem.FullPath.EndsWith(".local"))
            {
                return true;
            }

            return false;
        }

        public override void Rename(NodeLabelEditEventArgs e)
        {
            string oldName = Text;
            string oldFilePath = (DirectoryItem.FullPath + DirectoryItem.Extension).Replace(DirectoryItem.Extension, ".local" + DirectoryItem.Extension);

            base.Rename(e);

            if(!File.Exists(oldFilePath))
            {
                return;
            }

            string newFilePath = (DirectoryItem.FullPath + DirectoryItem.Extension).Replace(DirectoryItem.Extension, ".local" + DirectoryItem.Extension);

            File.Move(oldFilePath, newFilePath);
        }
    }
}
