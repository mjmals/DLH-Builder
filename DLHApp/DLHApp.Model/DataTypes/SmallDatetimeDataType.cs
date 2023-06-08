using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class SmallDatetimeDataType : DataType, ITimestampDataType
    {
        public override string[] DisplayNames => new string[] { "SmallDatetime", "SmallDatetimeDataType", "SmallDatetimeType" };

        public override object DefaultValue => new DateTime(1900, 1, 1).ToString("yyyy-MM-dd HH:mm:00");
    }
}
