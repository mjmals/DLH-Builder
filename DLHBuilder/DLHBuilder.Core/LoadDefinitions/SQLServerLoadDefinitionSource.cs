using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.LoadDefinitions
{
    public class SQLServerLoadDefinitionSource : LoadDefinitionSource
    {
        public SQLServerLoadDefinitionSource()
        {
            _type = DataFileFormat.MSSQL;
        }

        public string Server { get; set; }

        public string Database { get; set; }

        public string Schema { get; set; }

        public string Table { get; set; }
    }
}
