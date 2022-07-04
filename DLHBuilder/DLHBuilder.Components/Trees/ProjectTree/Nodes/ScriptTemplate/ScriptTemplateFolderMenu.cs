using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class ScriptTemplateFolderMenu : ProjectTreeMenu
    {
        public ScriptTemplateFolderMenu(ScriptTemplateFolderNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Template", AddTemplate));
            Items.Add(new ProjectTreeMenuButton("Add Template Folder", AddFolder));
        }

        ScriptTemplateFolderNode Node
        {
            get => (ScriptTemplateFolderNode)Tag;
            set => Tag = value;
        }

        void AddTemplate(object sender, EventArgs e)
        {
            ScriptTemplate template = new ScriptTemplate();
            template.ID = Guid.NewGuid();
            template.Name = "<New Script Template>";
            template.Type = Node.TemplateType;
            template.Content = string.Empty;
            template.Hierarchy = Node.Path.Split('.').ToList();

            Node.Tree.Project.ScriptTemplates.Add(template);

            ScriptTemplateNode node = new ScriptTemplateNode(template);
            Node.Nodes.Add(node);
            Node.Tree.SelectedNode = node;
        }

        void AddFolder(object sender, EventArgs e)
        {
            ScriptTemplateFolderNode foldernode = new ScriptTemplateFolderNode("<New Folder>", Node.TemplateType, Node.Path, Node.AllowUpdate);
            Node.Nodes.Add(foldernode);
            Node.Tree.SelectedNode = foldernode;
        }
    }
}
