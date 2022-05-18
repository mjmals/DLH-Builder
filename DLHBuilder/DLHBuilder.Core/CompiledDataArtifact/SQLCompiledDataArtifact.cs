using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    class SQLCompiledDataArtifact : CompiledDataArtifact, ICompiledDataArtifact
    {
        public SQLCompiledDataArtifact(DataArtifact artifact, MSSQLDataStage stage, DataArtifactReference reference) : base(artifact, stage, reference)
        {
            SQLStage = stage;
        }

        public MSSQLDataStage SQLStage { get; set; }

        public override string Name
        {
            get => string.Format("[{0}].[{1}]", SQLStage.Schema, Artifact.Name);
        }

        protected override ICompiledSchemaItem SetSchemaItem(DataArtifactSchemaItem schemaItem)
        {
            return new SQLCompiledSchemaItem(schemaItem, Reference.Transformations);
        }

        protected override ICompiledSchemaItem[] SetSchema()
        {
            return base.SetSchema();
        }
    }
}
