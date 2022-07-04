using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactSchemaItemsMenu : ProjectTreeMenu
    {
        public DataArtifactSchemaItemsMenu(DataArtifactSchemaItemsNode node)
        {
            Node = node;
            Items.Add(new ProjectTreeMenuButton("Add Schema Item", AddSchemaItem));
        }

        DataArtifactSchemaItemsNode Node
        {
            get => (DataArtifactSchemaItemsNode)Tag;
            set => Tag = value;
        }

        void AddSchemaItem(object sender, EventArgs e)
        {
            Node.Items.Add(DataArtifactSchemaItem.New());
        }
    }
}
