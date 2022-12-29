using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters.SQL
{
    class IntegerDataTypeSQLConverter : IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes => new Dictionary<Type, string[]>
        {
            { typeof(SQLDataConnection), new string[] { "int", "bigint", "smallint", "tinyint", "float", "real" } }
        };

        public IDataType ConvertSourceType(string sourceType, DataTypeConverterProperties properties)
        {
            switch(sourceType.ToLower())
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
    }
}
