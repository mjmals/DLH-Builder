using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactReferenceSchemaItemsNode : ProjectTreeNode
    {
        public DataArtifactReferenceSchemaItemsNode(DataArtifactSchemaItemCollection schemaItems, DataArtifactReference reference)
        {
            SchemaItems = schemaItems;
            Reference = reference;
            Text = "Schema";

            SchemaItems.CollectionModified += OnSchemaItemsModified;
            AddSchemaItems();
        }

        public DataArtifactReference Reference
        {
            get => (DataArtifactReference)Tag;
            set => Tag = value;
        }

        public DataArtifactSchemaItemCollection SchemaItems { get; set; }

        void OnSchemaItemsModified(object sender, EventArgs e)
        {
            Nodes.Clear();
            AddSchemaItems();
        }

        void AddSchemaItems()
        {
            foreach (DataArtifactSchemaItem schemaItem in Reference.ReferencedArtifact.Schema)
            {
                SchemaInclusionDataArtifactTransformation itemInclusion = (SchemaInclusionDataArtifactTransformation)Reference.Transformations.FirstOrDefault(x => x.GetType() == typeof(SchemaInclusionDataArtifactTransformation) && x.ReferencedObjectID == schemaItem.ID);

                if (itemInclusion != null && itemInclusion.Include)
                {
                    Nodes.Add(new DataArtifactReferenceSchemaItemNode(schemaItem, Reference));
                }
            }
        }
    }
}
