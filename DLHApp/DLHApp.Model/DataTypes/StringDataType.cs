using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataTypes
{
    public class StringDataType : DataType, IDataType
    {
        public StringDataType()
        {

        }

        public StringDataType(string length, string isUnicode, string isCaseSensitive, string isAccentSensitive)
        {
            Length = Convert.ToInt32(length);
            IsUnicode = Convert.ToBoolean(isUnicode.ToLower());
            IsCaseSensitive = Convert.ToBoolean(isCaseSensitive.ToLower());
            IsAccentSensitive = Convert.ToBoolean(isAccentSensitive.ToLower());
        }

        public StringDataType(string length, string isUnicode, string isCaseSensitive)
        {
            Length = Convert.ToInt32(length);
            IsUnicode = Convert.ToBoolean(isUnicode.ToLower());
            IsCaseSensitive = Convert.ToBoolean(isCaseSensitive.ToLower());
        }

        public StringDataType(string length, string isUnicode)
        {
            Length = Convert.ToInt32(length);
            IsUnicode = Convert.ToBoolean(isUnicode.ToLower());
        }

        public StringDataType(string length)
        {
            Length = Convert.ToInt32(length);
        }

        public override string[] DisplayNames => new string[] { "String", "StringDataType", "StringType" };

        public int Length { get; set; }

        public bool IsUnicode { get; set; }

        public bool IsCaseSensitive { get; set; }

        public bool IsAccentSensitive { get; set; }

        public override string FormattedValue()
        {
            if(IsAccentSensitive)
            {
                return string.Format("{0}({1},{2},{3},{4})", DisplayNames[0], Length, IsUnicode, IsCaseSensitive, IsAccentSensitive);
            }

            if(IsCaseSensitive)
            {
                return string.Format("{0}({1},{2},{3})", DisplayNames[0], Length, IsUnicode, IsCaseSensitive);
            }

            if(IsUnicode)
            {
                return string.Format("{0}({1},{2})", DisplayNames[0], Length, IsUnicode);
            }

            if(Length > 0)
            {
                return string.Format("{0}({1})", DisplayNames[0], Length);
            }

            return string.Format("{0}()", DisplayNames[0]);
        }
    }
}
