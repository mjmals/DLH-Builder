using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SQLCompiledSchemaItem : CompiledSchemaItem, ICompiledSchemaItem
    {
        public SQLCompiledSchemaItem(DataArtifactSchemaItemReference schemaRef, DataArtifactTransformationCollection transformations, int ordinal) : base(schemaRef, transformations, ordinal)
        {
            
        }
    }
}
