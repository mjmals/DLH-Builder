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
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Schema Item";

        public override string ExpandedImage => "Schema Item";

        public override EditorCollection Editors()
        {
            return null; //new EditorCollection(new SchemaItemTransformationEditor(Item.Transformations, Tree.Project.Stages));
        }

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
