using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactImportSchemaItemTree : TreeView
    {
        public DataArtifactImportSchemaItemTree(DataArtifact artifact, DataArtifactImportSelectionCollection selectedItems)
        {
            Artifact = artifact;
            SelectedItems = selectedItems;

            Dock = DockStyle.Fill;
            CheckBoxes = true;
            ImageList = Images.Items;
            ImageKey = "Schema Item";
            SelectedImageKey = "Schema Item";
            AfterCheck += SchemaItemChecked;

            if (Artifact != null && Artifact.Schema != null)
            {
                AddItems();
            }
        }

        DataArtifact Artifact { get; set; }

        DataArtifactImportSelectionCollection SelectedItems { get; set; }

        void AddItems()
        {
            foreach(DataArtifactSchemaItem item in Artifact.Schema)
            {
                TreeNode node = new TreeNode();
                node.Text = item.Name;
                node.Tag = item;
                node.Checked = false;

                if(SelectedItems.ContainsKey(Artifact) && SelectedItems[Artifact].IndexOf(item) > -1)
                {
                    node.Checked = true;
                }

                Nodes.Add(node);
            }
        }

        void SchemaItemChecked(object sender, TreeViewEventArgs e)
        {
            DataArtifactSchemaItem item = (DataArtifactSchemaItem)e.Node.Tag;

            switch(e.Node.Checked)
            {
                case true:
                    if(SelectedItems.ContainsKey(Artifact) && SelectedItems[Artifact].IndexOf(item) == -1)
                    {
                        SelectedItems[Artifact].Add(item);
                    }
                    break;
                case false:
                    if(SelectedItems.ContainsKey(Artifact) && SelectedItems[Artifact].IndexOf(item) > -1)
                    {
                        SelectedItems[Artifact].Remove(item);
                    }
                    break;
            }
        }
    }
}
