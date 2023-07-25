using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHApp.Model.DataStructs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataStructFieldKeyType
    {
        Primary,
        Foreign,
        Business,
        Surrogate,
        Version,
        Measure,
        Validation
    }
}
