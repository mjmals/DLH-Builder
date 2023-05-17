using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHWin.ProjectTree.NodeTypes;
using DLHWin.Editors;

namespace DLHWin.ProjectTree
{
    internal abstract class ProjectTreeNode : TreeNode
    {
        public ProjectTreeNode(ProjectDirectoryItem directoryItem)
        {
            DirectoryItem = directoryItem;
            Text = directoryItem.Name;
            Name = directoryItem.FullPath;
            ImageKey = Images[0];
            SelectedImageKey = Images[0];

            directoryItem.AllowChild = AllowChild;
            DirectoryItem.ParentChanged += DirectoryParentChanged;
        }

        public ProjectDirectoryItem DirectoryItem { get; set; }

        public IModelItem ModelItem { get; set; }

        protected virtual string[]? Images => new string[] { "Folder Closed", "Folder Open" };

        public virtual bool Ignore => false;

        protected virtual bool AllowChild => true;

        protected virtual bool AllowDelete => false;

        protected virtual bool AllowRename => true;

        protected Tree Tree => (Tree)this.TreeView;

        internal string GetNodeExpandedImage()
        {
            if (Images.Count() == 1)
            {
                return Images[0];
            }
            else
            {
                return Images[1];
            }
        }

        internal string GetNodeCollapsedImage()
        {
            return Images[0];
        }

        internal virtual bool ValidateType(ProjectDirectoryItem directoryItem)
        {
            return false;
        }

        public override ContextMenuStrip ContextMenuStrip => Menu();

        protected virtual ContextMenuStrip Menu()
        {
            ProjectTreeNodeMenu output = new ProjectTreeNodeMenu();
            
            if(AllowChild)
            {
                output.AddButton("Add Folder", AddFolder);
            }

            if(AllowDelete)
            {
                output.AddButton("Delete", DeleteNode);
            }

            return output;
        }

        public virtual EditorCollection Editors()
        {
            return null;
        }

        void AddFolder(object sender, EventArgs e)
        {
            ProjectDirectoryItem dirItem = new ProjectDirectoryItem();
            dirItem.Name = "New Folder";
            dirItem.Parent = DirectoryItem.FullPath;
            dirItem.Type = ProjectDirectoryItemType.Folder;

            int newFolderCounter = 0;

            foreach(TreeNode node in this.Nodes)
            {
                if(node.Name.StartsWith(dirItem.FullPath))
                {
                    newFolderCounter++;
                }
            }

            dirItem.Name = newFolderCounter > 0 ? string.Format("{0} ({1})", dirItem.Name, newFolderCounter) : dirItem.Name;

            Directory.CreateDirectory(dirItem.FullPath);
            Tree.AddNode(dirItem);
        }

        void DeleteNode(object sender, EventArgs e)
        {
            switch(DirectoryItem.Type)
            {
                case ProjectDirectoryItemType.Folder:
                    DeleteFolder();
                    break;
                case ProjectDirectoryItemType.File:
                    DeleteFile();
                    break;
            }
        }

        void DeleteFolder()
        {
            DialogResult prompt = MessageBox.Show(string.Format("This will delete {0} and all subfiles and subdirecories.  Continue?", DirectoryItem.FullPath), "Confirm Delete", MessageBoxButtons.OKCancel);

            if (prompt == DialogResult.OK)
            {
                Directory.Delete(DirectoryItem.FullPath, true);
                this.TreeView.Nodes.Remove(this);
            }
        }

        void DeleteFile()
        {
            DialogResult prompt = MessageBox.Show(string.Format("This will delete {0}{1}.  Continue?", DirectoryItem.FullPath, DirectoryItem.Extension), "Confirm Delete", MessageBoxButtons.OKCancel);

            if (prompt == DialogResult.OK)
            {
                File.Delete(DirectoryItem.FullPath + DirectoryItem.Extension);
                this.TreeView.Nodes.Remove(this);
            }
        }

        public virtual void Rename(NodeLabelEditEventArgs e)
        {
            if(!AllowRename)
            {
                e.CancelEdit = true;
                return;
            }

            string newName = e.Label;
            string oldPath = string.Empty;

            switch(DirectoryItem.Type)
            {
                case ProjectDirectoryItemType.Folder:
                    string dirPath = DirectoryItem.FullPath;
                    oldPath = DirectoryItem.FullPath;
                    
                    string folderFile = Directory.GetFiles(DirectoryItem.FullPath).FirstOrDefault(x => Path.GetFileNameWithoutExtension(x).StartsWith(DirectoryItem.Name));
                    
                    if(folderFile != null)
                    {
                        File.Move(folderFile, Path.Combine(DirectoryItem.FullPath, newName + folderFile.Substring(folderFile.IndexOf("."))));
                    }
                    
                    DirectoryItem.Name = newName;

                    string newPath = DirectoryItem.FullPath;
                    Directory.Move(dirPath, newPath);
                    break;
                case ProjectDirectoryItemType.File:
                    string filePath = DirectoryItem.FullPath + DirectoryItem.Extension;
                    oldPath = DirectoryItem.FullPath;
                    DirectoryItem.Name = newName;
                    string newFilePath = DirectoryItem.FullPath + DirectoryItem.Extension;
                    File.Move(filePath, newFilePath);
                    break;
            }

            foreach(ProjectDirectoryItem childItem in Tree.Project.Directory.Where(x => x.Parent == oldPath))
            {
                childItem.Parent = DirectoryItem.FullPath;
            }

            Text = newName;
            Name = DirectoryItem.FullPath;
        }

        void DirectoryParentChanged(object sender, ProjectDirectoryItemParentEventArgs e)
        {
            Name = DirectoryItem.FullPath;
        }
    }
}
