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

        public override Type[] DataTypes => this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IStringDataType))).ToArray();

        public override IDataType Import(string dataType)
        {
            dataType = dataType.ToLower();

            int length = 0;

            if (dataType.Contains("(") && dataType.Contains(")"))
            {
                int parmStart = dataType.IndexOf("(");
                int parmEnd = dataType.IndexOf(")");
                string param = dataType.Substring(parmStart + 1, parmEnd - parmStart - 1);

                length = Convert.ToInt32(param.ToUpper() == "MAX" ? -1 : param);
            }

            if(dataType.StartsWith("n"))
            {
                return new UnicodeStringDataType() { Length = length };
            }

            return new StringDataType() { Length = length };
        }

        public override string Export(IDataType dataType)
        {
            if(!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a string data type");
            }

            IStringDataType stringType = (IStringDataType)dataType;
            string length = stringType.Length <= 0 ? "MAX" : stringType.Length.ToString();
            
            if(stringType is UnicodeStringDataType)
            {
                return string.Format("nvarchar({0})", length);
            }

            return string.Format("varchar({0})", length);
        }
    }
}
