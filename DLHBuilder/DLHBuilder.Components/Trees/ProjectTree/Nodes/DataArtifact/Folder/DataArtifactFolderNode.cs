using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactFolderNode : ProjectTreeNode
    {
        public DataArtifactFolderNode(DataArtifactFolder folder)
        {
            Folder = folder;
            Text = Folder.Name;
            SetName();
        }

        public DataArtifactFolder Folder
        {
            get => (DataArtifactFolder)Tag;
            set => Tag = value;
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactFolderMenu(this);

        void SetName()
        {
            Name = "Data Artifacts." + Folder.FullPath;
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            CascadeRename(text);
            Folder.Name = text;
            SetName();
        }

        void CascadeRename(string newName)
        {
            string searchPath = Folder.FullPath + ".";
            int replaceIndex = Folder.Path.Count;

            foreach(DataArtifactFolder fldr in Tree.Project.ArtifactFolders.Where(x => x.FullPath.StartsWith(searchPath)))
            {
                fldr.Path[replaceIndex] = Text;
            }

            foreach(DataArtifact artifact in Tree.Project.Artifacts.Where(x => x.ArtifactPath.StartsWith(searchPath)))
            {
                artifact.ArtifactNamespace[replaceIndex] = Text;
            }
        }
    }
}
