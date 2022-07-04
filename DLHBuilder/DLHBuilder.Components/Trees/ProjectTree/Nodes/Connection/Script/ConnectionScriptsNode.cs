using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionScriptsNode : ProjectTreeNode
    {
        public ConnectionScriptsNode(ScriptTemplateReferenceCollection templates, IDataConnection connection)
        {
            Text = "Scripts";
            Templates = templates;
            Connection = connection;
            AddScripts(null, null);
        }

        public ScriptTemplateReferenceCollection Templates
        {
            get => (ScriptTemplateReferenceCollection)Tag;
            set
            {
                value.CollectionModified += AddScripts;
                Tag = value;
            }
        }

        IDataConnection Connection { get; set; }

        void AddScripts(object sender, EventArgs e)
        {
            Nodes.Clear();

            foreach(ScriptTemplateReference template in Templates)
            {
                Nodes.Add(new ConnectionScriptNode(template, Connection));
            }
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ScriptTemplateMappingEditor(Tree.Project.ScriptTemplates, Templates));
        }
    }
}
