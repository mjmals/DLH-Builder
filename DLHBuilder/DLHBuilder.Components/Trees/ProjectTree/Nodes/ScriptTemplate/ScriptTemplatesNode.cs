using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Generator;
using System.Reflection;
using DLHBuilder.Components.Model;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ScriptTemplatesNode : ProjectTreeNode
    {
        public ScriptTemplatesNode(ScriptTemplateCollection templates)
        {
            Text = "Script Templates";
            Templates = templates;
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

            ScriptTemplateCollection templates = new CollatedScriptTemplateCollection(Templates);

            foreach (ScriptTemplate template in templates)
            {
                string templatepath = string.Empty;

                foreach(string folder in template.Hierarchy)
                {
                    templatepath += templatepath == string.Empty ? folder : string.Format(".{0}", folder);

                    if(this.Nodes.Find(templatepath, true).Count() == 0)
                    {
                        string parentpath = templatepath.Contains(".") ? templatepath.Substring(0, templatepath.LastIndexOf(".")) : templatepath;
                        ScriptTemplateFolderNode parentnode = (ScriptTemplateFolderNode)this.Nodes.Find(parentpath, true).FirstOrDefault();
                        parentnode.Nodes.Add(new ScriptTemplateFolderNode(folder, parentnode.TemplateType, parentnode.Path, parentnode.AllowUpdate));
                    }
                }

                ScriptTemplateFolderNode foldernode = (ScriptTemplateFolderNode)this.Nodes.Find(string.Join('.', template.Path()), true).FirstOrDefault();
                foldernode.Nodes.Add(new ScriptTemplateNode(template));
            }
        }
    }
}
