using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class TimestampDataType : DataType, IDataType
    {
        public TimestampDataType()
        {
            Precision = TimestampDataTypePrecision.DateTime;
        }

        public TimestampDataType(string precision)
        {
            Precision = TimestampDataTypePrecision.DateTime;
            string[] precisionValues = Enum.GetNames<TimestampDataTypePrecision>();
            string precisionValue = precisionValues.FirstOrDefault(x => x.ToLower() == precision.ToLower());

            if (!string.IsNullOrEmpty(precisionValue))
            {
                Precision = (TimestampDataTypePrecision)Enum.Parse(typeof(TimestampDataTypePrecision), precisionValue);
            }
        }

        public override string[] DisplayNames => new string[] { "Timestamp", "TimestampDataType", "TimestampType" };

        public TimestampDataTypePrecision Precision { get; set; }

        public override string FormattedValue()
        {
            return string.Format("{0}(\"{1}\")", DisplayNames[0], Precision == TimestampDataTypePrecision.DateTime ? string.Empty : Enum.GetName(typeof(TimestampDataTypePrecision), Precision).ToLower());
        }
    }
}
