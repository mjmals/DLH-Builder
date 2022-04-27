using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactScriptNode : ProjectTreeNode
    {
        public DataArtifactScriptNode(ScriptTemplateReference reference, DataArtifact artifact)
        {
            Text = reference.DisplayName;
            Reference = reference;
            Artifact = artifact;
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
    }
}
