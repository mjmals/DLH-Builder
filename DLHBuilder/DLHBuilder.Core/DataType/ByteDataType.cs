using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ByteDataType : IDataType
    {
        public Type BaseType => typeof(byte);

        public override string ToString()
        {
            return "Byte";
        }
    }
}
