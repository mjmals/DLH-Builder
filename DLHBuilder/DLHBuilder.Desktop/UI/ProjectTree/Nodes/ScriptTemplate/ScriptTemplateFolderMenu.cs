using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class ScriptTemplateFolderMenu : ProjectTreeMenu
    {
        public ScriptTemplateFolderMenu(ScriptTemplateFolderNode node)
        {
            Node = node;
            Items.Add("Add Template");
            Items.Add(new ProjectTreeMenuButton("Add Template Folder", AddFolder));
        }

        ScriptTemplateFolderNode Node
        {
            get => (ScriptTemplateFolderNode)Tag;
            set => Tag = value;
        }

        void AddFolder(object sender, EventArgs e)
        {
            ScriptTemplateFolderNode foldernode = new ScriptTemplateFolderNode("<New Folder>", Node.Path, Node.AllowUpdate);
            Node.Nodes.Add(foldernode);
            Node.Tree.SelectedNode = foldernode;
        }
    }
}
