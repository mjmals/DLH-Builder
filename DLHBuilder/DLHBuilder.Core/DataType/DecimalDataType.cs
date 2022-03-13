using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType
{
    class DecimalDataType : IDataType
    {
        public Type BaseType => typeof(decimal);

        public int Precision { get; set; }

        public int Scale { get; set; }

        public override string ToString()
        {
            return "Decimal";
        }
    }
}
