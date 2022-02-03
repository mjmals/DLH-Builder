using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SQLDataConnection : DataConnection
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Authentication { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Server, Database);
        }
    }
}
