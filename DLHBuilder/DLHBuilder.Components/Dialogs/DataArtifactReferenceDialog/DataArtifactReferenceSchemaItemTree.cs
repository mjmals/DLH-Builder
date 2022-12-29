using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Components.Dialogs
{
    public class DataArtifactReferenceSchemaItemTree : TreeView
    {
        public DataArtifactReferenceSchemaItemTree(DataArtifactSchemaItemCollection schemaItems, DataArtifactSchemaItemReferenceCollection schemaRefs)
        {
            Dock = DockStyle.Fill;
            CheckBoxes = schemaRefs == null ? false : true;
            ImageList = Images.Items;
            ImageKey = "Schema Item";
            SelectedImageKey = "Schema Item";

            SchemaItems = schemaItems;
            SchemaReferences = schemaRefs;

            if(schemaItems != null)
            {
                AddSchemaItems();
            }

            AfterCheck += SchemaItemNodeChecked;
        }

        DataArtifactSchemaItemCollection SchemaItems { get; set; }

        DataArtifactSchemaItemReferenceCollection SchemaReferences { get; set; }

        void AddSchemaItems()
        {
            foreach(DataArtifactSchemaItem schemaItem in SchemaItems)
            {
                TreeNode node = new TreeNode();
                node.Text = schemaItem.Name;
                node.Checked = SchemaReferences == null ? false : SchemaReferences.Exists(x => x.SchemaItemID == schemaItem.ID);
                node.Tag = schemaItem;
                Nodes.Add(node);
            }
        }

        void AddSchemaItemReference(DataArtifactSchemaItem schemaItem)
        {
            DataArtifactSchemaItemReference schemaRef = new DataArtifactSchemaItemReference();
            schemaRef.ID = Guid.NewGuid();
            schemaRef.SchemaItemID = schemaItem.ID;
            schemaRef.ReferencedSchemaItem = schemaItem;

            SchemaReferences.Add(schemaRef);
        }

        void SchemaItemNodeChecked(object sender, TreeViewEventArgs e)
        {
            DataArtifactSchemaItem schemaItem = (DataArtifactSchemaItem)e.Node.Tag;

            switch(e.Node.Checked)
            {
                case true:
                    if(SchemaReferences != null && !SchemaReferences.Exists(x => x.SchemaItemID == schemaItem.ID))
                    {
                        AddSchemaItemReference(schemaItem);
                    }
                    break;
                case false:
                    if(SchemaReferences != null && SchemaReferences.Exists(x => x.SchemaItemID == schemaItem.ID))
                    {
                        SchemaReferences.Remove(SchemaReferences.FirstOrDefault(x => x.SchemaItemID == schemaItem.ID));
                    }
                    break;
            }
        }
    }
}
