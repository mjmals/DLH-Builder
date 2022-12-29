using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLHBuilder
{
    public abstract class ConnectionDataSource : DataSource
    {
        [Browsable(false)]
        public Guid ConnectionID { get; set; }
    }
}
