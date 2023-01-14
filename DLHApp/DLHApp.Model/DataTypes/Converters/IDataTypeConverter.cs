using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters
{
    public interface IDataTypeConverter
    {
        public string[] SourceTypeNames { get; }

        public Type[] DataTypes { get; }

        public DataTypeConverterType ConverterType { get; }

        public IDataType Import(string dataType);

        public string Export(IDataType dataType);
    }
}
