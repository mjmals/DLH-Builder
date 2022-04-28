using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactScriptsNode : ProjectTreeNode
    {
        public DataArtifactScriptsNode(DataArtifact artifact)
        {
            Text = "Scripts";
            Artifact = artifact;
            AddScripts();

            Artifact.ScriptTemplates.CollectionModified += OnTemplatesModified;
        }

        DataArtifact Artifact { get; set; }

        void OnTemplatesModified(object sender, EventArgs e)
        {
            Nodes.Clear();
            AddScripts();
        }

        void AddScripts()
        {
            foreach(ScriptTemplateReference reference in Artifact.ScriptTemplates)
            {
                Nodes.Add(new DataArtifactScriptNode(reference, Artifact));
            }
        }
    }
}
