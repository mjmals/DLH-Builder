using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DateTimeDataType : IDataType
    {
        public Type BaseType => typeof(DateTime);

        public DateTimePrecision Precision { get; set; }

        public override string ToString()
        {
            return Precision.ToString();
        }
    }
}
