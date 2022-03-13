using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class IntegerDataType : IDataType
    {
        public Type BaseType => typeof(int);

        public IntegerSize Size { get; set; }

        public override string ToString()
        {
            return "Integer";
        }
    }
}
