using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class LoadDefinitionSource
    {
        public DataFileFormat Type { get => _type; }

        protected DataFileFormat _type { get; set; }
    }
}
