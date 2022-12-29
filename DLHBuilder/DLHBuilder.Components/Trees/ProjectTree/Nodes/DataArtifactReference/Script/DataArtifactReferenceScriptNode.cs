using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceScriptNode : ProjectTreeNode
    {
        public DataArtifactReferenceScriptNode(ScriptTemplateReference template, DataArtifactReference reference, IDataStage stage, IDataApplication application)
        {
            Reference = reference;
            Template = template;
            Stage = stage;
            Application = application;
            Text = Template.DisplayName;

            Template.PropertyUpdated += OnReferenceUpdated;
        }

        ScriptTemplateReference Template { get; set; }

        DataArtifactReference Reference { get; set; }

        IDataStage Stage { get; set; }

        IDataApplication Application { get; set; }

        public override string ExpandedImage => "Script";

        public override string CollapsedImage => "Script";

        public override EditorCollection Editors()
        {
            DataArtifactCompiler compiler = new DataArtifactCompiler(Reference.ReferencedArtifact, Application, Stage, Tree.Project);

            return new EditorCollection
                (
                    new ScriptViewer(compiler, Template, Tree.Project.ScriptTemplates)
                );
        }

        void OnReferenceUpdated(object sender, EventArgs e)
        {
            Text = Template.DisplayName;
        }
    }
}
