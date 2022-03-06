using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactSchemaItemNode : ProjectTreeNode
    {
        public DataArtifactSchemaItemNode(DataArtifactSchemaItem item)
        {
            Item = item;
            Text = Item.Name;
        }

        public DataArtifactSchemaItem Item
        {
            get => (DataArtifactSchemaItem)Tag;
            set
            {
                Tag = value;
            }
        }

        public override string CollapsedImage => "Schema Item";

        public override string ExpandedImage => "Schema Item";
    }
}
