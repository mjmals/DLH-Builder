using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ConnectionScriptNode : ProjectTreeNode
    {
        public ConnectionScriptNode(ScriptTemplateReference template, IDataConnection connection)
        {
            Template = template;
            Connection = connection;

            Text = template.DisplayName;
            template.PropertyUpdated += SetTitle;
        }

        ScriptTemplateReference Template
        {
            get => (ScriptTemplateReference)Tag;
            set => Tag = value;
        }

        IDataConnection Connection { get; set; }

        public override string ExpandedImage => "Script";

        public override string CollapsedImage => "Script";

        public override bool AllowLabelChange => false;

        void SetTitle(object sender, EventArgs e)
        {
            Text = Template.DisplayName;
        }

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ScriptViewer(Connection, Template, Tree.Project.ScriptTemplates));
        }
    }
}
