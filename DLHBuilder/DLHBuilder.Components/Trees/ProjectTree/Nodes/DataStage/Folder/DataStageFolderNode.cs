using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataStageFolderNode : ProjectTreeNode
    {
        public DataStageFolderNode(DataStageFolder folder, IDataStage parentStage, IDataApplication parentApplication)
        {
            Folder = folder;
            ParentStage = parentStage;
            ParentApplication = parentApplication;
            Text = folder.Name;
        }

        public DataStageFolder Folder
        {
            get => (DataStageFolder)Tag;
            set => Tag = value;
        }

        public IDataStage ParentStage { get; set; }

        public IDataApplication ParentApplication { get; set; }

        public override ContextMenuStrip ContextMenuStrip => new DataStageFolderMenu(this);

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);

            List<string> path = Name.Split('.').ToList();
            int pos = path.IndexOf(path.Last());
            path[pos] = text;

            CascadeRename(text);
            Folder.Name = text;
            Name = string.Join('.', path);
        }

        void CascadeRename(string newName)
        {
            string searchPath = Folder.FullPath + ".";
            int replaceIndex = Folder.Path.Count();
            Console.WriteLine(ParentStage.Folders.Where(x => x.FullPath.StartsWith(searchPath)));
            foreach(DataStageFolder folder in ParentStage.Folders.Where(x => x.FullPath.StartsWith(searchPath)))
            {
                folder.Path[replaceIndex] = newName;
            }

            foreach(DataArtifactReference reference in ParentStage.ArtifactReferences.Where(x => x.FullPath.StartsWith(searchPath)))
            {
                reference.Path[replaceIndex] = newName;
            }
        }
    }
}
