using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class CompiledSchemaItem : ICompiledSchemaItem
    {
        public CompiledSchemaItem(DataArtifactSchemaItem schemaItem, DataArtifactTransformationCollection transformations)
        {
            SchemaItem = schemaItem;
            Transformations = new DataArtifactTransformationCollection();
            Transformations.AddRange(transformations.Where(x => x.ReferencedObjectID == schemaItem.ID));
            Convert();
        }

        public DataArtifactSchemaItem SchemaItem { get; set; }

        public DataArtifactTransformationCollection Transformations { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public bool IsNullable { get; set; }

        public virtual string Definition { get; set; }

        public int Ordinal { get; set; }

        protected virtual string GetDataType()
        {
            return SchemaItem.DataType.ToString();
        }

        protected T GetTransformation<T>()
        {
            return (T)Transformations.FirstOrDefault(x => x.GetType() == typeof(T));
        }

        protected virtual string GetDefinition()
        {
            DefinitionDataArtifactTransformation definition = GetTransformation<DefinitionDataArtifactTransformation>();

            if(definition != null)
            {
                return definition.Definition;
            }

            return string.Empty;
        }

        protected virtual void Convert()
        {
            Name = SchemaItem.Name;
            DataType = GetDataType();
            IsNullable = SchemaItem.IsNullable;
            Definition = GetDefinition();
        }
    }
}
