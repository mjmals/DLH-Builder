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
            Folder.Name = text;
            SetName();
        }
    }
}
