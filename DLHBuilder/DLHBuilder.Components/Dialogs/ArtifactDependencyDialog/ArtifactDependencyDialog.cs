using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public abstract class ArtifactDependencyDialog : Form
    {
        public ArtifactDependencyDialog()
        {
            Text = "Add Dependency";
            Height = 400;
            Width = 800;

            Controls.Add(SelectionTree);
            Controls.Add(ControlPanel);
            ControlPanel.Controls.Add(AddButton);

            SelectionTree.AfterSelect += OnNodeSelected;
            AddButton.Click += OnAdd;
        }

        public List<IDataArtifactDependency> SelectedDependencies = new List<IDataArtifactDependency>();

        protected TreeView SelectionTree = new TreeView() { Dock = DockStyle.Fill, ImageList = Images.Items };

        protected Panel ControlPanel = new Panel() { Height = 50, Dock = DockStyle.Bottom };

        protected Button AddButton = new Button() { Text = "Add Selected", Width = 200, Enabled = false, Dock = DockStyle.Right };

        protected virtual void OnNodeSelected(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Name == SelectionAllowedType().Name)
            {
                AddButton.Enabled = true;
            }
            else
            {
                AddButton.Enabled = false;
            }
        }

        protected TreeNode RootNode(string key)
        {
            return SelectionTree.Nodes.Find(key, true).FirstOrDefault();
        }

        protected virtual Type SelectionAllowedType()
        {
            return null;
        }

        protected List<IDataArtifactDependency> ConvertSelected()
        {
            List<IDataArtifactDependency> output = new List<IDataArtifactDependency>();

            if(SelectionTree.CheckBoxes == true)
            {
                foreach(TreeNode node in SelectionTree.Nodes.Find(SelectionAllowedType().Name, true).Where(x => x.Checked == true))
                {
                    output.Add((IDataArtifactDependency)node.Tag);
                }
            }
            else
            {
                output.Add((IDataArtifactDependency)SelectionTree.SelectedNode.Tag);
            }

            return output;
        }

        protected void OnAdd(object sender, EventArgs e)
        {
            SelectedDependencies.AddRange(ConvertSelected());

            DialogResult = DialogResult.OK;
        }
    }
}
