using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class StringDataType : IDataType
    {
        public StringDataType()
        {

        }

        public StringDataType(string length = "-1", string isUnicode = "false", string isCaseSensitive = "false", string isAccentSensitive = "false")
        {
            Length = Convert.ToInt32(length);
            IsUnicode = Convert.ToBoolean(isUnicode.ToLower());
            IsCaseSensitive = Convert.ToBoolean(isCaseSensitive.ToLower());
            IsAccentSensitive = Convert.ToBoolean(isAccentSensitive.ToLower());
        }

        public string[] DisplayNames => new string[] { "String", "StringDataType", "StringType" };

        public int Length { get; set; }

        public bool IsUnicode { get; set; }

        public bool IsCaseSensitive { get; set; }

        public bool IsAccentSensitive { get; set; }

        public string FormattedValue()
        {
            return string.Format("{0}({1})", DisplayNames[0], Length <= 0 ? string.Empty : Length);
        }
    }
}
