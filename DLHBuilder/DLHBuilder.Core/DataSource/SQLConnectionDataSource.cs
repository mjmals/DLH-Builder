using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SQLConnectionDataSource : ConnectionDataSource
    {
        public string Schema { get; set; }

        public string Table { get; set; }

        public static SQLConnectionDataSource New(Guid connectionid)
        {
            SQLConnectionDataSource output = new SQLConnectionDataSource();
            output.ID = Guid.NewGuid();
            output.Name = "<New SQL Data Source>";
            output.ConnectionID = connectionid;

            return output;
        }
    }
}
