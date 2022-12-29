using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder.DataType
{
    public class DecimalDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(decimal);

        public int Precision { get; set; }

        public int Scale { get; set; }

        public override string ToString()
        {
            return "Decimal";
        }

        public string FormattedName()
        {
            return string.Format("{0}({1},{2})",ToString(),Precision, Scale);
        }

    }
}
