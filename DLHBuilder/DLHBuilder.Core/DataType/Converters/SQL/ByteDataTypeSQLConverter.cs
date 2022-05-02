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
            { typeof(SQLDataConnection), new string[] { "binary" } }
        };

        public IDataType ConvertSourceType(string sourceType)
        {
            ByteDataType output = new ByteDataType();
            string length = sourceType.Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray().Last();
            output.Length = length.ToLower() == "max" ? 8000 : Convert.ToInt32(length);

            return output;
        }
    }
}
