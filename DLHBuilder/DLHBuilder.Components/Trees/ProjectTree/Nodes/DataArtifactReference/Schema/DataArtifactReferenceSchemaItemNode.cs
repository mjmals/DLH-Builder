using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Components.Editors;
using DLHBuilder.Components.EditorGrids;

namespace DLHBuilder.Components.Trees.ProjectTreeView
{
    class DataArtifactReferenceSchemaItemNode : ProjectTreeNode
    {
        public DataArtifactReferenceSchemaItemNode(DataArtifactSchemaItemReference schemaRef, DataArtifactReference reference)
        {
            SchemaRef = schemaRef;
            Reference = reference;

            Text = SchemaRef.ReferencedSchemaItem.Name;

            foreach(CodeDefinition codeDef in SchemaRef.Definitions)
            {
                codeDef.DefinitionSetName = Reference.DefinitionSets.FirstOrDefault(x => x.ID == codeDef.DefinitionSetID)?.Name;
            }
        }

        public DataArtifactSchemaItemReference SchemaRef
        {
            get => (DataArtifactSchemaItemReference)Tag;
            set => Tag = value;
        }

        public DataArtifactReference Reference { get; set; }

        public override string ExpandedImage => "Schema Item";

        public override string CollapsedImage => "Schema Item";

        public override bool AllowLabelChange => false;

        public override EditorCollection Editors()
        {
            return new EditorCollection(
                new CodeDefinitionEditorGrid(SchemaRef.Definitions.ToArray())
            );
        }
    }
}
