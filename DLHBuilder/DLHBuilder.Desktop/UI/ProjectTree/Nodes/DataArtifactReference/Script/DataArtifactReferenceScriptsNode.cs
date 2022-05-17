using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactReferenceScriptsNode : ProjectTreeNode
    {
        public DataArtifactReferenceScriptsNode(DataArtifactReference reference, IDataStage stage, IDataApplication application)
        {
            Text = "Scripts";
            Reference = reference;
            Stage = stage;
            Application = application;
            AddScripts();

            Reference.ScriptTemplates.CollectionModified += OnTemplatesModified;
        }

        DataArtifactReference Reference { get; set; }

        IDataStage Stage { get; set; }

        IDataApplication Application { get; set; }

        void OnTemplatesModified(object sender, EventArgs e)
        {
            Nodes.Clear();
            AddScripts();
        }

        void AddScripts()
        {
            foreach (ScriptTemplateReference template in Reference.ScriptTemplates)
            {
                Nodes.Add(new DataArtifactReferenceScriptNode(template, Reference, Stage, Application));
            }
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection
                (
                    new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Reference.ScriptTemplates)
                );
        }
    }
}
