using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class CompiledSchemaItem : ICompiledSchemaItem
    {
        public CompiledSchemaItem(DataArtifactSchemaItemReference schemaRef, DataArtifactTransformationCollection transformations, int ordinal)
        {
            SchemaRef = schemaRef;
            Convert();
            Ordinal = ordinal;
            IsLast = false;
        }

        public DataArtifactSchemaItemReference SchemaRef { get; set; }

        public string Name { get; set; }

        public string OriginalName { get; set; }

        public string DataType { get; set; }

        public string DataTypeFormatted { get; set; }

        public bool IsNullable { get; set; }

        public DataArtifactSchemaItemKeyType KeyType { get; set; }

        public virtual string Definition => Definitions == null ? string.Empty : Definitions.FirstOrDefault();

        public virtual string[] Definitions { get; set; }

        public int Ordinal { get; set; }

        public bool IsLast { get; set; }

        protected virtual string GetDataType()
        {
            return SchemaRef.ReferencedSchemaItem.DataType.ToString();
        }

        protected virtual string GetDataTypeFormatted()
        {
            return SchemaRef.ReferencedSchemaItem.DataType.FormattedName().ToString();
        }


        protected virtual void Convert()
        {
            Name = SchemaRef.ReferencedSchemaItem.Name;
            DataType = GetDataType();
            DataTypeFormatted = GetDataTypeFormatted();
            IsNullable = SchemaRef.ReferencedSchemaItem.IsNullable;
            KeyType = SchemaRef.ReferencedSchemaItem.KeyType;

            if(SchemaRef.ReferencedSchemaItem.Transformations.Exists(x => x is RenameDataArtifactTransformation))
            {
                RenameDataArtifactTransformation transformation = (RenameDataArtifactTransformation)SchemaRef.ReferencedSchemaItem.Transformations.FirstOrDefault(x => x is RenameDataArtifactTransformation);
                OriginalName = transformation.OriginalName;
            }

            List<string> definitions = new List<string>();

            foreach(CodeDefinition codeDef in SchemaRef.Definitions)
            {
                definitions.Add(codeDef.Code);
            }

            Definitions = definitions.ToArray();
        }
    }
}
