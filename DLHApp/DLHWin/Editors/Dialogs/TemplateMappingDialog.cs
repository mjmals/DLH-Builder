using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHWin.Styles;

namespace DLHWin.Editors.Dialogs
{
    internal class TemplateMappingDialog : Form
    {
        public TemplateMappingDialog(TemplateReferenceCollection templateRefs)
        {
            Text = "Edit Template Mappings";
            Height = 500;
            Width = 700;

            TemplateRefs = templateRefs;
            Controls.Add(GetTemplateTree());
        }

        TemplateReferenceCollection TemplateRefs { get; set; }

        TreeView GetTemplateTree()
        {
            TreeView output = new TreeView();
            output.Dock = DockStyle.Fill;
            output.CheckBoxes = true;
            output.ImageList = Images.List;
            output.Nodes.Add(new TreeNode() { Name = "Templates", ImageKey = "Folder Closed", SelectedImageKey = "Folder Closed", Text = "Templates" });

            GetTemplates(Path.Combine(Environment.CurrentDirectory, "Templates"), output.Nodes[0]);

            output.Nodes[0].Expand();

            output.AfterCheck += OnNodeChecked;
            return output;
        }

        void GetTemplates(string directory, TreeNode parentNode)
        {
            string parentPath = directory.Replace(Environment.CurrentDirectory + @"\Templates\", "");

            foreach(string subDir in Directory.GetDirectories(directory))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(subDir);

                TreeNode dirNode = new TreeNode()
                {
                    Name = Path.Combine(parentPath, dirInfo.Name),
                    Text = dirInfo.Name,
                    ImageKey = "Folder Closed",
                    SelectedImageKey = "Folder Closed"
                };

                parentNode.Nodes.Add(dirNode);

                GetTemplates(subDir, dirNode);
            }

            foreach(string file in Directory.GetFiles(directory))
            {
                TreeNode scriptNode = new TreeNode() 
                {
                    Name = Path.Combine(parentPath, Path.GetFileNameWithoutExtension(file)),
                    Text = Path.GetFileNameWithoutExtension(file),
                    ImageKey = "Script",
                    SelectedImageKey = "Script"
                };

                scriptNode.Checked = TemplateRefs.Exists(x => x == scriptNode.Name);
                parentNode.Nodes.Add(scriptNode);
            }
        }

        void OnNodeChecked(object sender, TreeViewEventArgs e)
        {
            foreach(TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }

            if(e.Node.ImageKey == "Script")
            {
                if(e.Node.Checked && !TemplateRefs.Exists(x => x == e.Node.Name))
                {
                    TemplateRefs.Add(e.Node.Name);
                }

                if (!e.Node.Checked && TemplateRefs.Exists(x => x == e.Node.Name))
                {
                    TemplateRefs.Remove(e.Node.Name);
                }
            }
        }
    }
}
