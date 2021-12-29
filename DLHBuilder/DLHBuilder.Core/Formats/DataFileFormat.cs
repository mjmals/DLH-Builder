using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DLHBuilder
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataFileFormat
    {
        MSSQL,
        JSON,
        CSV,
        XML,
        HTML,
        XLS,
        Parquet,
        Delta
    }
}
