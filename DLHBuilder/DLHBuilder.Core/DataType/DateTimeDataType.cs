using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    public class DateTimeDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(DateTime);

        [JsonConverter(typeof(StringEnumConverter))]
        public DateTimePrecision Precision { get; set; }

        public override string ToString()
        {
            return Precision.ToString();
        }
        public string FormattedName()
        {
            return ToString();
        }

    }
}
