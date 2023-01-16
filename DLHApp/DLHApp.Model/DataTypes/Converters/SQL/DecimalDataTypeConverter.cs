using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class DecimalDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "decimal" };

        public override Type[] DataTypes => new Type[] { typeof(DecimalDataType) };

        public override IDataType Import(string dataType)
        {
            DecimalDataType output = new DecimalDataType();

            dataType = dataType.ToLower();

            output.Precision = 18;
            output.Scale = 2;

            if (dataType.Contains("(") && dataType.Contains(")"))
            {
                int parmStart = dataType.IndexOf("(");
                int parmEnd = dataType.IndexOf(")");
                string param = dataType.Substring(parmStart + 1, parmEnd - parmStart - 1);

                string[] typeParams = param.Trim().Replace(" ", "").Split(",");
                output.Precision = Convert.ToInt32(typeParams[0]);
                output.Scale = typeParams.Length > 0 ? Convert.ToInt32(typeParams[1]) : 0;
            }

            return output;
        }

        public override string Export(IDataType dataType)
        {
            if(!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a decimal data type");
            }

            DecimalDataType decimalType = (DecimalDataType)dataType;
            return string.Format("decimal({0},{1})", decimalType.Precision, decimalType.Scale);
        }
    }
}
