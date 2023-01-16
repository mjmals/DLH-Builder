using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    internal class NumericDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "numeric" };

        public override Type[] DataTypes => new Type[] { typeof(NumericDataType) };

        public override IDataType Import(string dataType)
        {
            NumericDataType output = new NumericDataType();

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
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a numeric data type");
            }

            NumericDataType numericType = (NumericDataType)dataType;
            return string.Format("numeric({0},{1})", numericType.Precision, numericType.Scale);
        }
    }
}
