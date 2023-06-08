using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataTypes
{
    public class StringDataType : DataType, IStringDataType, IDataType
    {
        public StringDataType()
        {
            
        }

        public StringDataType(string length)
        {
            Length = Convert.ToInt32(length);
        }

        public override string[] DisplayNames => new string[] { "String", "StringDataType", "StringType" };

        public int Length { get; set; }

        public override string FormattedValue()
        {
            if(Length > 0)
            {
                return string.Format("{0}({1})", DisplayNames[0], Length);
            }

            return string.Format("{0}()", DisplayNames[0]);
        }

        public override object DefaultValue => string.Empty;
    }
}
