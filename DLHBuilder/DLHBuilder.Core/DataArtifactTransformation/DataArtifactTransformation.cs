using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public abstract class DataArtifactTransformation : IDataArtifactTransformation
    {
        [Browsable(false)]
        public Guid ID { get; set; }

        [Browsable(false)]
        public virtual string Title => string.Empty;

        [Browsable(false)]
        public Guid ReferencedObjectID { get; set; }

        [Browsable(false)]
        public virtual string Name
        {
            get
            {
                return ID.ToString();
            }
        }
    }
}
