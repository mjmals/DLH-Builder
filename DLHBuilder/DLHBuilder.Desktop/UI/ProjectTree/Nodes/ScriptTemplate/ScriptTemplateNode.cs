using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ScriptTemplateNode : ProjectTreeNode
    {
        public ScriptTemplateNode(ScriptTemplate template)
        {
            Text = template.Name;
        }

        public override string ExpandedImage => "Template";

        public override string CollapsedImage => "Template";
    }
}
