using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactImportObjectTree : TreeView
    {
        public DataArtifactImportObjectTree(DataConnection connection, DataArtifactCollection artifacts)
        {
            Dock = DockStyle.Fill;
            CheckBoxes = true;
            this.AfterCheck += NodeChecked;

            Connection = connection;
            Artifacts = artifacts;

            ImageList = Images.Items;
            Nodes.Add(Connection.Name, Connection.Name, "Connection", "Connection");
            AddFolders();
            AddArtifacts();
        }

        DataConnection Connection { get; set; }

        DataArtifactCollection Artifacts { get; set; }

        void AddFolders()
        {
            foreach(DataArtifact artifact in Artifacts.OrderBy(x => x.FullName))
            {
                string nodePath = Nodes[0].Name;

                foreach(string hierarchy in artifact.ArtifactNamespace)
                {
                    string nodeParent = nodePath;
                    nodePath += "." + hierarchy;

                    if(Nodes.Find(nodePath, true).FirstOrDefault() == null)
                    {
                        TreeNode parentNode = Nodes.Find(nodeParent, true).FirstOrDefault();
                        parentNode.Nodes.Add(nodePath, hierarchy, "Folder Closed", "Folder Closed");
                        parentNode.Expand();
                    }
                }
            }
        }

        void AddArtifacts()
        {
            foreach(DataArtifact artifact in Artifacts.OrderBy(x => x.FullName))
            {
                TreeNode parentNode = Nodes.Find(Connection.Name + "." + artifact.ArtifactPath, true).FirstOrDefault();
                TreeNode node = new TreeNode();
                node.Text = artifact.Name;
                node.ImageKey = "Data Artifact";
                node.SelectedImageKey = "Data Artifact";
                node.Tag = artifact;
                parentNode.Nodes.Add(node);
            }
        }

        void NodeChecked(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Nodes.Count > 0)
            {
                SetNodeChecked(e.Node, e.Node.Checked);
            }
        }

        void SetNodeChecked(TreeNode node, bool isChecked)
        {
            foreach(TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
            }
        }
    }
}
