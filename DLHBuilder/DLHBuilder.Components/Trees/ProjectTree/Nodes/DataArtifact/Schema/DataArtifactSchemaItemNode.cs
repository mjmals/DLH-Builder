using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactSchemaItemNode : ProjectTreeNode
    {
        public DataArtifactSchemaItemNode(DataArtifactSchemaItem item, DataSourceCollection sources)
        {
            Item = item;
            Sources = sources;
            Text = Item.Name;
        }

        public DataArtifactSchemaItem Item
        {
            get => (DataArtifactSchemaItem)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public DataSourceCollection Sources { get; set; }

        public override string CollapsedImage => "Schema Item";

        public override string ExpandedImage => "Schema Item";

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Item.Name;
        }

        public override void LabelChanged(string text)
        {
            Item.Name = text;
            base.LabelChanged(text);
        }
    }
}
