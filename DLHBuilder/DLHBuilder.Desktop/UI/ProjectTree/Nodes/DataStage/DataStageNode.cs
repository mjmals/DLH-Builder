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
            AddFolders();
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
                new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Stage.ScriptTemplates),
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
            ProjectTreeNode node = this;

            if(!string.IsNullOrEmpty(artifact.ArtifactPath))
            {
                node = (ProjectTreeNode)Nodes.Find(artifact.ArtifactPath, true).FirstOrDefault();
            }
            
            node.Nodes.Add(output);
            return output;
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in Stage.Artifacts)
            {
                AddArtifact(artifact);
            }
        }

        void AddFolders()
        {
            foreach(DataArtifact artifact in Stage.Artifacts)
            {
                DataArtifactFolderNode parent = null;
                string path = "";

                foreach (string folder in artifact.ArtifactNamespace)
                {
                    TreeNode findnode = Nodes.Find(path == string.Empty ? "#" : path, true).FirstOrDefault();
                    parent = findnode != null ? (DataArtifactFolderNode)findnode : parent;
                    path += path == string.Empty ? folder : string.Format(".{0}", folder);

                    if (Nodes.Find(path, true).Count() > 0)
                    {
                        continue;
                    }

                    DataArtifactFolderNode node = new DataArtifactFolderNode(folder, parent, this);

                    switch (parent != null)
                    {
                        case true:
                            parent.Nodes.Add(node);
                            break;
                        default:
                            this.Nodes.Add(node);
                            break;
                    }

                    parent = node;
                }
            }
        }
    }
}
