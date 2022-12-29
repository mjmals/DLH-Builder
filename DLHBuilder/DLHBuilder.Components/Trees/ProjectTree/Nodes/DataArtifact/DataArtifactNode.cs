using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactNode : ProjectTreeNode
    {
        public DataArtifactNode(DataArtifact artifact)
        {
            Artifact = artifact;

            Text = Artifact.Name;
            Nodes.Add(new DataSourcesNode(artifact.DataSources));
            Nodes.Add(new DataArtifactSchemaItemsNode(artifact.Schema, artifact.DataSources));
            //Nodes.Add(new DataArtifactScriptsNode(artifact));
        }

        public DataArtifact Artifact
        {
            get => (DataArtifact)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Data Artifact";

        public override string ExpandedImage => "Data Artifact";

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Artifact.Name;
        }

        public override void LabelChanged(string text)
        {
            Artifact.Name = text;
            base.LabelChanged(text);
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection
                (
                    new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Artifact.ScriptTemplates)
                );
        }
    }
}
