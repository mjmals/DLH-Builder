using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class DataConnection
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
    }
}
