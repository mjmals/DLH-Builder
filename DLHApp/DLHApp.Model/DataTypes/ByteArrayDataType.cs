using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class ByteArrayDataType : DataType, IDataType
    {
        public ByteArrayDataType()
        {

        }

        public ByteArrayDataType(string length)
        {
            Length = Convert.ToInt32(length);
        }

        public override string[] DisplayNames => new string[] { "ByteArray", "ByteArrayDataType", "ByteArrayType" };

        public int Length { get; set; }

        public override string FormattedValue()
        {
            return base.FormattedValue();
        }
    }
}
