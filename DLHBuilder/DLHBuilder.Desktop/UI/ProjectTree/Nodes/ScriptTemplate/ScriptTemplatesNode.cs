using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ScriptTemplatesNode : ProjectTreeNode
    {
        public ScriptTemplatesNode(ScriptTemplateCollection templates)
        {
            Text = "Script Templates";
            AddTemplates();
        }

        ScriptTemplateCollection Templates
        {
            get => (ScriptTemplateCollection)Tag;
            set => Tag = value;
        }

        void AddTemplates()
        {
            foreach(string templatetype in Enum.GetNames(typeof(ScriptTemplateType)))
            {
                ScriptTemplateType type = (ScriptTemplateType)Enum.Parse(typeof(ScriptTemplateType), templatetype);
                bool allowupdate = type == ScriptTemplateType.BuiltIn ? false : true; 
                Nodes.Add(new ScriptTemplateFolderNode(templatetype, type, string.Empty, allowupdate));
            }
        }
    }
}
