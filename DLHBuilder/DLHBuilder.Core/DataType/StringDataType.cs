using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class StringDataType : IDataType
    {
        public Type BaseType => typeof(string);

        public int Length { get; set; }

        public bool IsUnicode { get; set; }

        public override string ToString()
        {
            return "String";
        }
    }
}
