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

            return "datetime";
        }
    }
}
