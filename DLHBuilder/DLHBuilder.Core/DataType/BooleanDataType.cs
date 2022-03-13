using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataType
{
    public class BooleanDataType : IDataType
    {
        public Type BaseType => typeof(bool);

        public override string ToString()
        {
            return "Boolean";
        }
    }
}
