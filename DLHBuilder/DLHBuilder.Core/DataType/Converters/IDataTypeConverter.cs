using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType.Converters
{
    public interface IDataTypeConverter
    {
        public Dictionary<Type, string[]> SourceTypes { get; }

        public IDataType ConvertSourceType(string sourceType, DataTypeConverterProperties properties);
    }
}
