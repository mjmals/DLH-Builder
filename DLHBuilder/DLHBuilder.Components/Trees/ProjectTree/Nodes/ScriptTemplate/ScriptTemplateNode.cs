using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Editors;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ScriptTemplateNode : ProjectTreeNode
    {
        public ScriptTemplateNode(ScriptTemplate template)
        {
            Template = template;
            Text = template.Name;
        }

        public ScriptTemplate Template
        {
            get => (ScriptTemplate)Tag;
            set => Tag = value;
        }

        public override string ExpandedImage => "Template";

        public override string CollapsedImage => "Template";

        public override EditorCollection Editors()
        {
            return new EditorCollection(new ScriptTemplateEditor(Template, "Content", (Template.Type == ScriptTemplateType.BuiltIn ? false : true)));
        }

        public override void LabelChanged(string text)
        {
            base.LabelChanged(text);

            Template.Name = text;
        }
    }
}
