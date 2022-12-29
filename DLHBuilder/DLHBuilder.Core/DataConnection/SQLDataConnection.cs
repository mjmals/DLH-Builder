using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class SQLDataConnection : DataConnection, IDataConnection
    {
        [Browsable(false)]
        public override string Name
        {
            get
            {
                if(string.IsNullOrEmpty(name))
                {
                    name = string.Format("{0}_{1}", Server, Database);
                }

                return name;
            }
            set
            {
                OnPropertyUpdated(Name, name, value);
                name = value;
            }
        }

        private string name { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AuthenticationType Authentication { get; set; }

        public string ConnectionString()
        {
            return string.Format("Integrated Security=True;Data Source={0};Initial Catalog={1}", Server, Database);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
