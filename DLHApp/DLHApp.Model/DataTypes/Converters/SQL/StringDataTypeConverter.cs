using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class StringDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "char", "nchar", "varchar", "nvarchar", "text", "ntext" };

        public override Type[] DataTypes => new Type[] { typeof(StringDataType) };

        public override IDataType Import(string dataType)
        {
            StringDataType output = new StringDataType();

            dataType = dataType.ToLower();

            int parmStart = dataType.IndexOf("(");
            int parmEnd = dataType.IndexOf(")");
            string param = dataType.Substring(parmStart + 1, parmEnd - parmStart - 1);

            output.Length = Convert.ToInt32(param == "MAX" ? -1 : param);
            output.IsUnicode = dataType.StartsWith("n") ? true : false;
            output.IsCaseSensitive = dataType.Contains("_cs") ? true : false ;
            output.IsAccentSensitive = dataType.Contains("_as") ? true : false;

            return output;
        }

        public override string Export(IDataType dataType)
        {
            if(!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a string data type");
            }

            string output = string.Empty;

            StringDataType stringType = (StringDataType)dataType;

            output += stringType.IsUnicode ? "n" : "";
            output += string.Format("varchar({0})", stringType.Length <= 0 ? "MAX" : stringType.Length.ToString());
            
            if(stringType.IsCaseSensitive || stringType.IsAccentSensitive)
            {
                output += " COLLATE Latin1_General_{0}_{1}";
                output = string.Format(output, stringType.IsCaseSensitive ? "CS" : "CI", "{0}");
                output = string.Format(output, stringType.IsAccentSensitive ? "AS" : "AI");
            }

            return output;
        }
    }
}
