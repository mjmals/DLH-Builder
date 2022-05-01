using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataTypeConverter
    {
        static string[] SQLStringTypes = new string[] { "char", "text" };
        static string[] SQLIntegerTypes = new string[] { "int" };
        static string[] SQLDatetimeTypes = new string[] { "datetime", "date", "time" };

        public static IDataType ConvertToDataType(string sourceType)
        {
            sourceType = sourceType.ToLower();

            if(SQLStringTypes.Any(x => x.Contains(sourceType)))
            {
                return SQLStringType(sourceType);
            }

            if(SQLIntegerTypes.Any(x => x.EndsWith(sourceType)))
            {
                return SQLIntegerType(sourceType);
            }

            if(SQLDatetimeTypes.Any(x => x.Contains(sourceType)))
            {
                return SQLDatetimeType(sourceType);
            }

            return null;
        }

        static StringDataType SQLStringType(string sourceType)
        {
            StringDataType output = new StringDataType();
            output.IsUnicode = sourceType.StartsWith("n") ? true : false;
            
            string length = sourceType.Split('(', ')').Last();
            output.Length = length == "max" ? 8000 : Convert.ToInt32(length);

            return output;
        }

        static IntegerDataType SQLIntegerType(string sourceType)
        {
            switch (sourceType)
            {
                case "bigint":
                    return new IntegerDataType() { Size = IntegerSize.Long };
                case "smallint":
                    return new IntegerDataType() { Size = IntegerSize.Small };
                case "tinyint":
                    return new IntegerDataType() { Size = IntegerSize.Tiny };
                default:
                    return new IntegerDataType() { Size = IntegerSize.Normal };
            }
        }

        static DateTimeDataType SQLDatetimeType(string sourceType)
        {
            switch(sourceType)
            {
                case "date":
                    return new DateTimeDataType() { Precision = DateTimePrecision.Date };
                case "time":
                    return new DateTimeDataType() { Precision = DateTimePrecision.Time };
                default:
                    return new DateTimeDataType() { Precision = DateTimePrecision.DateTime };
            }
        }
    }
}
