using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    class ADLSCompiledSchemaItem : CompiledSchemaItem, ICompiledSchemaItem
    {
        public ADLSCompiledSchemaItem(DataArtifactSchemaItem schemaItem, DataArtifactTransformationCollection transformations, int ordinal) : base(schemaItem, transformations, ordinal)
        {

        }
    }
}
