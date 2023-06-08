using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class UnicodeStringDataType : StringDataType, IStringDataType, IDataType
    {
        public UnicodeStringDataType()
        {

        }

        public UnicodeStringDataType(string length) : base(length)
        {

        }

        public override string[] DisplayNames => new string[] { "UnicodeString", "UnicodeStringDataType", "UnicodeStringType" };

        public override object DefaultValue => string.Empty;
    }
}
