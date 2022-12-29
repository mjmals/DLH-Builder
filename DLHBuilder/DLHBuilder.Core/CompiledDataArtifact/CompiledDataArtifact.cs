using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class CompiledDataArtifact : ICompiledDataArtifact
    {
        public CompiledDataArtifact(DataArtifact artifact, IDataStage stage, DataArtifactReference reference, Project project)
        {
            Artifact = artifact;
            Stage = stage;
            Reference = reference;
            Project = project;
            Path = artifact.ArtifactNamespace.ToArray();
            Schema = SetSchema();
        }

        public DataArtifact Artifact { get; set; }

        public IDataStage Stage { get; set; }

        public DataArtifactReference Reference { get; set; }

        public Project Project { get; set; }

        public virtual string Name { get; }

        public string[] Path { get; set; }

        public string FullPath()
        {
            return string.Format("{0}.{1}", string.Join('.', Path), Name);
        }

        public ICompiledSchemaItem[] Schema { get; set; }

        protected virtual ICompiledSchemaItem SetSchemaItem(DataArtifactSchemaItemReference schemaRef, int ordinal)
        {
            return null;
        }

        protected virtual ICompiledSchemaItem[] SetSchema()
        {
            List<ICompiledSchemaItem> schema = new List<ICompiledSchemaItem>();

            int ordinal = 1;

            foreach (DataArtifactSchemaItemReference schemaRef in Reference.SchemaItemReferences)
            {
                schema.Add(SetSchemaItem(schemaRef, ordinal));
                ordinal++;
            }

            schema.Last().IsLast = true;

            return schema.ToArray();
        }
    }
}
