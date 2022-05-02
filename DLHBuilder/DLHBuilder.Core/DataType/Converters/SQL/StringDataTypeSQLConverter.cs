using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters.SQL
{
    class StringDataTypeSQLConverter : IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes => new Dictionary<Type, string[]>
        {
            { typeof(SQLDataConnection), new string[] { "char", "text" } }
        };

        public IDataType ConvertSourceType(string sourceType)
        {
            StringDataType output = new StringDataType();
            output.IsUnicode = sourceType.ToLower().StartsWith("n") ? true : false;
            string length = sourceType.Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray().Last();
            output.Length = length.ToLower() == "max" ? 8000 : Convert.ToInt32(length);

            return output;
        }
    }
}
