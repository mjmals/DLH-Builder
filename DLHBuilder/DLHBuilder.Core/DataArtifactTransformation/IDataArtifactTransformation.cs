using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataArtifactTransformation
    {
        public Guid ID { get; set; }

        public string Title { get; }

        public Guid ReferencedObjectID { get; set; }

        public string Name { get; }
    }
}
