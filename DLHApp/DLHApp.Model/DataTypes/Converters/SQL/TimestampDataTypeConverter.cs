using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class TimestampDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "datetime", "date", "time" };

        public override Type[] DataTypes => new Type[] { typeof(TimestampDataType) };

        public override IDataType Import(string dataType)
        {
            dataType = dataType.ToLower();

            TimestampDataType output = new TimestampDataType() { Precision = TimestampDataTypePrecision.DateTime };

            if (dataType == "date")
            {
                output.Precision = TimestampDataTypePrecision.Date;
            }

            if (dataType == "time")
            {
                output.Precision = TimestampDataTypePrecision.Time;
            }

            return output;
        }

        public override string Export(IDataType dataType)
        {
            string output = "datetime";

            TimestampDataType timestampType = (TimestampDataType)dataType;

            if (timestampType.Precision == TimestampDataTypePrecision.Date)
            {
                output = "date";
            }

            if (timestampType.Precision == TimestampDataTypePrecision.Time)
            {
                output = "time";
            }

            return output;
        }
    }
}
