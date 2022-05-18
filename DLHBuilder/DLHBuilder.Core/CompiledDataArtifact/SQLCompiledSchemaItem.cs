using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SQLCompiledSchemaItem : CompiledSchemaItem, ICompiledSchemaItem
    {
        public SQLCompiledSchemaItem(DataArtifactSchemaItem schemaItem, DataArtifactTransformationCollection transformations, int ordinal) : base(schemaItem, transformations, ordinal)
        {
            
        }
    }
}
