using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            treeModel.AfterSelect += NodeSelected;

            TreeNode projectnode = new TreeNode() { Text = "Lakehouse Project 1", Tag = new Project() { Name = "Lakehouse Project 1" } };
            treeModel.Nodes.Add(projectnode);

            TreeNode confignode = new TreeNode() { Text = "Configuration Options" };
            projectnode.Nodes.Add(confignode);

            confignode.Nodes.Add(new TreeNode() { Text = "Landing" });
            confignode.Nodes.Add(new TreeNode() { Text = "Bronze" });
            confignode.Nodes.Add(new TreeNode() { Text = "Silver" });
            confignode.Nodes.Add(new TreeNode() { Text = "Gold" });
        }

        void NodeSelected(object sender, EventArgs e)
        {
            TreeView view = (TreeView)sender;
            propertyGrid1.SelectedObject = view.SelectedNode.Tag;
        }

        public class Project
        {
            public string Name { get; set; }
        }
    }
}
