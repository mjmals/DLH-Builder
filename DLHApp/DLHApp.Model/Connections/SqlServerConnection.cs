using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHApp.Model.Connections
{
    public class SqlServerConnection : Connection, IModelItem
    {
        public override string? Name => string.IsNullOrEmpty(base.Name) ? string.Format("{0}_{1}", (string.IsNullOrEmpty(Server) ? string.Empty : Server), (string.IsNullOrEmpty(Database) ? string.Empty : Database)) : base.Name;

        public override string OutputExtension => "sqlcon.json";

        public string? Server { get; set; }

        public string? Database { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SqlServerAuthenticationType Authentication { get; set; }

        public new static SqlServerConnection New()
        {
            return new SqlServerConnection();
        }

        public static SqlServerConnection Load(string filePath)
        {
            SqlServerConnection output = JsonConvert.DeserializeObject<SqlServerConnection>(File.ReadAllText(filePath));
            output.Name = Path.GetFileName(filePath).Replace(".sqlcon.json", "");
            return output;
        }
    }
}
