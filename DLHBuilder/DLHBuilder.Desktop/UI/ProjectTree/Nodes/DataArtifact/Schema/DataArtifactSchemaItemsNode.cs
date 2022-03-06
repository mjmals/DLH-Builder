﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactSchemaItemsNode : ProjectTreeNode
    {
        public DataArtifactSchemaItemsNode(DataArtifactSchemaItemCollection items)
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

        public override ContextMenuStrip ContextMenuStrip => new DataArtifactSchemaItemsMenu(this);

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
            DataArtifactSchemaItemNode output = new DataArtifactSchemaItemNode(item);
            Nodes.Add(output);
            return output;
        }
    }
}
