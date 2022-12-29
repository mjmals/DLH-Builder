using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SourceDefinition : Definition, IDefinition
    {
        public string Name { get; set; }

        public SourceDefinitionSourceType SourceType { get; set; }
    }
}
