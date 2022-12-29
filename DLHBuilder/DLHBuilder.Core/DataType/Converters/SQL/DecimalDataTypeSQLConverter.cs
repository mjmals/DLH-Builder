using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters.SQL
{
    class DecimalDataTypeSQLConverter : IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes => new Dictionary<Type, string[]>
        {
            { typeof(SQLDataConnection), new string[] { "decimal", "numeric", "money" } }
        };

        public IDataType ConvertSourceType(string sourceType, DataTypeConverterProperties properties)
        {
            DecimalDataType output = new DecimalDataType();
            string sizeOptions = sourceType.Trim().Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray().Last();

            if (!sourceType.ToLower().EndsWith("money"))
            {
                output.Precision = 19;
                output.Scale = 4;
            }

            return output;
        }
    }
}
