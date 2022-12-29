using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ADLSCompiledDataArtifact : CompiledDataArtifact, ICompiledDataArtifact
    {
        public ADLSCompiledDataArtifact(DataArtifact artifact, ADLSDataStage stage, DataArtifactReference reference, Project project) : base(artifact, stage, reference, project)
        {
            ADLSStage = stage;
            Path = reference.Path.ToArray();
        }

        public ADLSDataStage ADLSStage { get; set; }

        public string[] Path { get; set; }

        public override string Name
        {
            get => Artifact.Name;
        }

        protected override ICompiledSchemaItem SetSchemaItem(DataArtifactSchemaItemReference schemaRef, int ordinal)
        {
            return new ADLSCompiledSchemaItem(schemaRef, Reference.Transformations, ordinal);
        }

        protected override ICompiledSchemaItem[] SetSchema()
        {
            return base.SetSchema();
        }
    }
}
