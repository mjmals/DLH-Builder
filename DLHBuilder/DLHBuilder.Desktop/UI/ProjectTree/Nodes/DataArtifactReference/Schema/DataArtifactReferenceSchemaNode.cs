using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactReferenceSchemaNode : ProjectTreeNode
    {
        public DataArtifactReferenceSchemaNode(DataArtifactSchemaItem schemaItem, DataArtifactReference reference)
        {
            SchemaItem = schemaItem;
            Reference = reference;

            Text = SchemaItem.Name;
        }

        public DataArtifactSchemaItem SchemaItem
        {
            get => (DataArtifactSchemaItem)Tag;
            set => Tag = value;
        }

        public DataArtifactReference Reference { get; set; }

        public override string ExpandedImage => "Schema Item";

        public override string CollapsedImage => "Schema Item";

        public override bool AllowLabelChange => false;
    }
}
