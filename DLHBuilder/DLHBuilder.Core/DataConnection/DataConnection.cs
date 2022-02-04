using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public abstract class DataConnection
    {
        [Browsable(false)]
        public Guid ID { get; set; }

        public virtual string Name { get; set; }
    }
}
