using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SourceDataType : IDataType
    {
        public SourceDataType(string name)
        {
            DataTypeName = name;
        }

        public Type BaseType => typeof(object);

        public string DataTypeName { get; set; }

        public DataTypeConverterProperties Properties { get; set; }

        public string FormattedName()
        {
            return ToString();
        }

    }
}
