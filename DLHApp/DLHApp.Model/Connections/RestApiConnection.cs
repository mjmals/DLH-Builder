using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHApp.Model.Connections
{
    public class RestApiConnection : Connection, IModelItem
    {
        public override string OutputExtension => "restcon.json";

        public string BaseUrl { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RestApiRequestMethod RequestMethod { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RestApiAuthenticationType AuthenticationType { get; set; }

        public static RestApiConnection Load(string filePath)
        {
            RestApiConnection output = JsonConvert.DeserializeObject<RestApiConnection>(File.ReadAllText(filePath));
            output.Name = Path.GetFileName(filePath).Replace(".restcon.json", "");
            return output;
        }
    }
}
