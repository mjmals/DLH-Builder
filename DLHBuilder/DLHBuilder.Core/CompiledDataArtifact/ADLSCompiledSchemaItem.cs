using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    class ADLSCompiledSchemaItem : CompiledSchemaItem, ICompiledSchemaItem
    {
        public ADLSCompiledSchemaItem(DataArtifactSchemaItemReference schemaRef, DataArtifactTransformationCollection transformations, int ordinal) : base(schemaRef, transformations, ordinal)
        {

        }
    }
}
