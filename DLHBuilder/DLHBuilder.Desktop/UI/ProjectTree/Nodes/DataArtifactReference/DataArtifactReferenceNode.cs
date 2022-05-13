using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{ 
    class DataArtifactReferenceNode : ProjectTreeNode
    {
        public DataArtifactReferenceNode(DataArtifactReference reference)
        {
            Reference = reference;
            Text = reference.ReferencedArtifact.Name;

            AddSchemaItems();
        }

        public DataArtifactReference Reference
        {
            get => (DataArtifactReference)Tag;
            set => Tag = value;
        }

        public override string ExpandedImage => "Data Artifact";

        public override string CollapsedImage => "Data Artifact";

        void AddSchemaItems()
        {
            foreach(DataArtifactSchemaItem schemaItem in Reference.ReferencedArtifact.Schema)
            {
                SchemaInclusionDataArtifactTransformation itemInclusion = (SchemaInclusionDataArtifactTransformation)Reference.Transformations.FirstOrDefault(x => x.GetType() == typeof(SchemaInclusionDataArtifactTransformation) && x.ReferencedObjectID == schemaItem.ID);

                if(itemInclusion != null && itemInclusion.Include)
                {
                    Nodes.Add(new DataArtifactReferenceSchemaNode(schemaItem, Reference));
                }
            }
        }
    }
}
