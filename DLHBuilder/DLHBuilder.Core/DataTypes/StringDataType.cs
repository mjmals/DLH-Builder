using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace DLHBuilder.DataTypes
{
    public class StringDataType : DataType
    {
        public StringDataType()
        {

        }

        public int Length { get; set; }

        public bool IsUnicode { get; set; }

        public override DataType ConvertTo(string type, DataItemFormat format)
        {
            StringDataType output = null;

            switch(format)
            {
                case DataItemFormat.MSSQL:
                    output = new StringDataType();
                    output.Length = Convert.ToInt32(Regex.Match(type, @"\d+").Value);
                    output.IsUnicode = type.ToUpper().StartsWith("N");
                    break;
                default:
                    break;
            }

            return output;
        }
    }
}
