using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters.SQL
{
    class DateTimeDataTypeSQLConverter : IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes => new Dictionary<Type, string[]>
        {
            { typeof(SQLDataConnection), new string[] { "datetime", "date", "time" } }
        };

        public IDataType ConvertSourceType(string sourceType, DataTypeConverterProperties properties)
        {
            DateTimeDataType output = new DateTimeDataType();
            
            switch(sourceType.ToLower())
            {
                case "datetime":
                    output.Precision = DateTimePrecision.DateTime;
                    break;
                case "datetime2":
                    output.Precision = DateTimePrecision.DateTime;
                    break;
                case "date":
                    output.Precision = DateTimePrecision.Date;
                    break;
                case "time":
                    output.Precision = DateTimePrecision.Time;
                    break;
            }

            return output;
        }
    }
}
