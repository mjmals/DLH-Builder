using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class LoadDefinitionTarget
    {
        public LoadDefinitionTarget(DataLayer targetlayer)
        {
            Layer = targetlayer;
        }

        public DataFileFormat Type { get => _type; }

        protected DataFileFormat _type { get; set; }

        public DataLayer Layer { get; set; }

        public virtual string FullPath()
        {
            return null;
        }
    }
}
