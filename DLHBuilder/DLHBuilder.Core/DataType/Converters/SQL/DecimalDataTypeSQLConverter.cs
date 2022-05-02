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
            { typeof(SQLDataConnection), new string[] { "decimal", "numeric" } }
        };

        public IDataType ConvertSourceType(string sourceType)
        {
            DecimalDataType output = new DecimalDataType();
            string sizeOptions = sourceType.Trim().Split('(', ')').Where(x => !string.IsNullOrEmpty(x)).ToArray().Last();
            output.Scale = Convert.ToInt32(sizeOptions.Split(",").First());
            output.Precision = Convert.ToInt32(sizeOptions.Split(",").Last());

            return output;
        }
    }
}
