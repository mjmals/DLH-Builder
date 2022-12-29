using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Components.Trees.ProjectTreeView
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
            //foreach (DataArtifactSchemaItem schemaItem in Reference.ReferencedArtifact.Schema)
            //{
            //    SchemaInclusionDataArtifactTransformation itemInclusion = (SchemaInclusionDataArtifactTransformation)Reference.Transformations.FirstOrDefault(x => x.GetType() == typeof(SchemaInclusionDataArtifactTransformation) && x.ReferencedObjectID == schemaItem.ID);

            //    if (itemInclusion != null && itemInclusion.Include)
            //    {
            //        Nodes.Add(new DataArtifactReferenceSchemaItemNode(schemaItem, Reference));
            //    }
            //}

            // placeholder - to be removed
            if(Reference.Transformations.Where(x => x.GetType() == typeof(SchemaInclusionDataArtifactTransformation)).Count() > 0 && Reference.SchemaItemReferences.Count() == 0)
            {
                foreach(SchemaInclusionDataArtifactTransformation schemaInclusion in Reference.Transformations.Where(x => x.GetType() == typeof(SchemaInclusionDataArtifactTransformation)))
                {
                    DataArtifactSchemaItem schemaItem = SchemaItems.FirstOrDefault(x => x.ID == schemaInclusion.ReferencedObjectID);

                    if (schemaItem != null)
                    {
                        DataArtifactSchemaItemReference schemaRef = new DataArtifactSchemaItemReference();
                        schemaRef.ID = Guid.NewGuid();
                        schemaRef.SchemaItemID = schemaItem.ID;
                        schemaRef.ReferencedSchemaItem = schemaItem;

                        DefinitionDataArtifactTransformation refDefinition = (DefinitionDataArtifactTransformation)Reference.Transformations.FirstOrDefault(x => x.ReferencedObjectID == schemaItem.ID && x.GetType() == typeof(DefinitionDataArtifactTransformation));

                        if(refDefinition != null)
                        {
                            CodeDefinition definition = new CodeDefinition();
                            definition.ID = Guid.NewGuid();
                            definition.Code = refDefinition.Definition;
                            definition.DefinitionSetID = Reference.DefinitionSets.FirstOrDefault(x => x.Name == "Default").ID;

                            schemaRef.Definitions.Add(definition);
                        }

                        Reference.SchemaItemReferences.Add(schemaRef);
                    }
                }
            }

            foreach(DataArtifactSchemaItemReference schemaRef in Reference.SchemaItemReferences)
            {
                schemaRef.ReferencedSchemaItem = schemaRef.ReferencedSchemaItem == null ? SchemaItems.FirstOrDefault(x => x.ID == schemaRef.SchemaItemID) : schemaRef.ReferencedSchemaItem;
                Nodes.Add(new DataArtifactReferenceSchemaItemNode(schemaRef, Reference));
            }
        }
    }
}
