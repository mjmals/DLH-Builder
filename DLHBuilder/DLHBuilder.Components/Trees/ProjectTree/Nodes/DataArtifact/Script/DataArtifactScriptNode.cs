using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactScriptNode : ProjectTreeNode
    {
        public DataArtifactScriptNode(ScriptTemplateReference reference, DataArtifact artifact)
        {
            Text = reference.DisplayName;
            Reference = reference;
            Artifact = artifact;

            Reference.PropertyUpdated += OnReferenceUpdated;
        }

        ScriptTemplateReference Reference { get; set; }

        DataArtifact Artifact { get; set; }

        public override string ExpandedImage => "Script";

        public override string CollapsedImage => "Script";

        public override EditorCollection Editors()
        {
            return new EditorCollection
                (
                    new ScriptViewer(Artifact, Reference, Tree.Project.ScriptTemplates)
                );
        }

        void OnReferenceUpdated(object sender, EventArgs e)
        {
            Text = Reference.DisplayName;
        }
    }
}
