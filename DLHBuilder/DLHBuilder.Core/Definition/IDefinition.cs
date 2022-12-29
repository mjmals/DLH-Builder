using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDefinition
    {
        public Guid ID { get; set; }

        public Guid DefinitionSetID { get; set; }
    }
}
