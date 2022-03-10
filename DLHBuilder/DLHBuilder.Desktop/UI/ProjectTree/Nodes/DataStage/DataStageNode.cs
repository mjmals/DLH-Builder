using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataStageNode : ProjectTreeNode
    {
        public DataStageNode(IDataStage stage)
        {
            Stage = stage;
            Text = stage.Name;
            AddArtifacts();
        }

        public IDataStage Stage
        {
            get => (IDataStage)Tag;
            set
            {
                value.Artifacts.CollectionAdded += OnArtifactAdded;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Data Stage";

        public override string ExpandedImage => "Data Stage";

        public override ContextMenuStrip ContextMenuStrip => new DataStageMenu(this);

        public override EditorCollection Editors()
        {
            return new EditorCollection
            (
                new PropertyEditor(Stage)
            );
        }

        public override void LabelChanged(string text)
        {
            Stage.Name = text;
            base.LabelChanged(text);
        }

        void OnArtifactAdded(object sender, EventArgs e)
        {
            DataArtifactNode node = AddArtifact((DataArtifact)sender);
            Tree.SelectedNode = node;
        }

        DataArtifactNode AddArtifact(DataArtifact artifact)
        {
            DataArtifactNode output = new DataArtifactNode(artifact);
            Nodes.Add(output);
            return output;
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in Stage.Artifacts)
            {
                AddArtifact(artifact);
            }
        }
    }
}
