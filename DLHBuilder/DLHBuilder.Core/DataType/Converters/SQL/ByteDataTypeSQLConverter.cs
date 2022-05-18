using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters.SQL
{
    class ByteDataTypeSQLConverter : IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes => new Dictionary<Type, string[]>
        {
            { typeof(SQLDataConnection), new string[] { "binary", "image" } }
        };

        public IDataType ConvertSourceType(string sourceType, DataTypeConverterProperties properties)
        {
            ByteDataType output = new ByteDataType();

            if (sourceType.ToLower() != "image")
            {
                string length = sourceType.Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray().Last();
                output.Length = length.ToLower() == "max" ? 8000 : Convert.ToInt32(length);
            }

            return output;
        }
    }
}
