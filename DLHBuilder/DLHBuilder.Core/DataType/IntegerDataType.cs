using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class IntegerDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(int);

        [JsonConverter(typeof(StringEnumConverter))]
        public IntegerSize Size { get; set; }

        public override string ToString()
        {
            return "Integer";
        }

        public string FormattedName()
        {
            return ToString();
        }

    }
}
