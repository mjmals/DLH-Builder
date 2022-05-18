using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class CompiledDataArtifact : ICompiledDataArtifact
    {
        public CompiledDataArtifact(DataArtifact artifact, IDataStage stage, DataArtifactReference reference)
        {
            Artifact = artifact;
            Stage = stage;
            Reference = reference;
            Schema = SetSchema();
        }

        public DataArtifact Artifact { get; set; }

        public IDataStage Stage { get; set; }

        public DataArtifactReference Reference { get; set; }

        public virtual string Name { get; }

        public ICompiledSchemaItem[] Schema { get; set; }

        protected virtual ICompiledSchemaItem SetSchemaItem(DataArtifactSchemaItem schemaItem, int ordinal)
        {
            return null;
        }

        protected virtual ICompiledSchemaItem[] SetSchema()
        {
            List<ICompiledSchemaItem> schema = new List<ICompiledSchemaItem>();

            DataArtifactSchemaItem[] schemaItems =
                Artifact.Schema
                .Where(s => Reference.Transformations.Exists(r => r.ReferencedObjectID == s.ID && r.GetType() == typeof(SchemaInclusionDataArtifactTransformation)))
                .ToArray();

            int ordinal = 1;

            foreach (DataArtifactSchemaItem schemaItem in schemaItems)
            {
                schema.Add(SetSchemaItem(schemaItem, ordinal));
                ordinal++;
            }

            schema.Last().IsLast = true;

            return schema.ToArray();
        }
    }
}
