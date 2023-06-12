using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class TimestampDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "datetime", "date", "time", "smalldatetime" };

        public override Type[] DataTypes => this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(ITimestampDataType))).ToArray();

        public override IDataType Import(string dataType)
        {
            dataType = dataType.ToLower();

            switch(dataType)
            {
                case "date":
                    return new DateDataType();
                case "time":
                    return new TimeDataType();
                case "smalldatetime":
                    return new SmallDatetimeDataType();
                default:
                    return new TimestampDataType();
            }
        }

        public override string Export(IDataType dataType)
        {
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a timestamp data type");
            }

            if (dataType is DateDataType)
            {
                return "date";
            }

            if(dataType is TimeDataType)
            {
                return "time";
            }

            if(dataType is SmallDatetimeDataType)
            {
                return "smalldatetime";
            }

            return "datetime";
        }

        public override string GetDefaultValue(IDataType dataType)
        {
            if(dataType is TimeDataType)
            {
                return "00:00:00.000";
            }

            return "1900-01-01";
        }
    }
}
