using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class MasterDataArtifact : IDataArtifact
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public DataArtifactSchemaItemCollection Schema { get; set; }
    }
}
