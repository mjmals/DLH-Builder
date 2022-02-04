using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.ComponentModel;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class SQLDataConnection : DataConnection
    {
        [JsonIgnore]
        [Browsable(false)]
        public override string Name { get => string.Format("{0}_{1}", Server, Database); }

        public string Server { get; set; }

        public string Database { get; set; }

        public AuthenticationType Authentication { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
