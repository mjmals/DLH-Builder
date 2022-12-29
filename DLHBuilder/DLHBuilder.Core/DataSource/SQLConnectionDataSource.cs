using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class SQLConnectionDataSource : ConnectionDataSource
    {
        public string Schema { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SQLConnectionDataSourceType ObjectType { get; set; }

        public string ObjectName { get; set; }

        public string Table 
        { 
            get => ObjectType == SQLConnectionDataSourceType.Table ? ObjectName : string.Empty; 
            set => ObjectName = string.IsNullOrEmpty(value) ? ObjectName : value; 
        }

        public string StoredProcedure 
        { 
            get => ObjectType == SQLConnectionDataSourceType.StoredProcedure ? ObjectName : string.Empty; 
            set => ObjectName = string.IsNullOrEmpty(value) ? ObjectName : value; 
        }

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
