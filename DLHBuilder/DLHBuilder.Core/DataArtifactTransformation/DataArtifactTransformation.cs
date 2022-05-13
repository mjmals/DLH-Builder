using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class DataArtifactTransformation : IDataArtifactTransformation
    {
        public Guid ID { get; set; }

        public virtual string Title => string.Empty;

        public Guid ReferencedObjectID { get; set; }

        public virtual string Name
        {
            get
            {
                return ID.ToString();
            }
        }
    }
}
