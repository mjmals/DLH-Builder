using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactFolderNode : ProjectTreeNode
    {
        public DataArtifactFolderNode(string folder, DataArtifactFolderNode parentfolder, DataStageNode parentstage)
        {
            Text = folder;
            ParentFolder = parentfolder;
            ParentStage = parentstage;

            Name = FolderPath();
        }

        public DataArtifactFolderNode ParentFolder { get; set; }

        public DataStageNode ParentStage { get; set; }

        public string FolderPath()
        {
            string output = Text;

            DataArtifactFolderNode parent = ParentFolder;

            while(parent != null)
            {
                output = string.Format("{0}.{1}", parent.Text, output);
                parent = parent.ParentFolder;
            }

            return output;
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactFolderMenu(this);

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);
            Name = FolderPath();
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in ParentStage.Stage.Artifacts.Where(x => x.ArtifactPath == FolderPath()))
            {
                Nodes.Add(new DataArtifactNode(artifact));
            }
        }
    }
}
