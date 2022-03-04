using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactGroupNode : ProjectTreeNode
    {
        public DataArtifactGroupNode(DataArtifactGroup group)
        {
            Text = group.Name;
            Group = group;

            AddArtifacts();
        }

        public DataArtifactGroup Group
        {
            get => (DataArtifactGroup)Tag;
            set
            {
                value.Artifacts.ArtifactAdded += OnArtifactAdded;
                Tag = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactGroupMenu(this);

        void OnArtifactAdded(object sender, DataArtifactEventArgs e)
        {
            AddArtifactNode(e.Artifact);
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in Group.Artifacts)
            {
                AddArtifactNode(artifact);
            }
        }

        void AddArtifactNode(DataArtifact artifact)
        {
            Nodes.Add(new DataArtifactNode(artifact));
        }

        public override void LabelChanged(string text)
        {
            Group.Name = text;
        }
    }
}
