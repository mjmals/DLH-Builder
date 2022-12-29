using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLHBuilder.Components.Editors;
using DLHBuilder.Components.EditorGrids;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactSchemaItemsNode : ProjectTreeNode
    {
        public DataArtifactSchemaItemsNode(DataArtifactSchemaItemCollection items, DataSourceCollection sources)
        {
            Text = "Schema";
            Items = items;
            AddItems();
        }

        public DataArtifactSchemaItemCollection Items
        {
            get => (DataArtifactSchemaItemCollection)Tag;
            set
            {
                value.CollectionAdded += OnCollectionAdded;
                Tag = value;
            }
        }

        DataSourceCollection Sources { get; set; }

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactSchemaItemsMenu(this);

        public override EditorCollection Editors()
        {
            return new EditorCollection(new DataArtifactSchemaEditorGrid(Items.ToArray()));
        }

        void OnCollectionAdded(object sender, EventArgs e)
        {
            DataArtifactSchemaItemNode node = AddItem((DataArtifactSchemaItem)sender);
            Tree.SelectedNode = node;
        }

        void AddItems()
        {
            foreach(DataArtifactSchemaItem item in Items)
            {
                AddItem(item);
            }
        }

        DataArtifactSchemaItemNode AddItem(DataArtifactSchemaItem item)
        {
            DataArtifactSchemaItemNode output = new DataArtifactSchemaItemNode(item, Sources);
            Nodes.Add(output);
            return output;
        }
    }
}
