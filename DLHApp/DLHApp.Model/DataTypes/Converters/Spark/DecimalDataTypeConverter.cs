using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class DecimalDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "DecimalType" };

        public override Type[] DataTypes => new Type[] { typeof(DecimalDataType), typeof(NumericDataType), typeof(MoneyDataType) };

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
            int precision = 18;
            int scale = 2;

            if(dataType is DecimalDataType)
            {
                precision = ((DecimalDataType)dataType).Precision;
                scale = ((DecimalDataType)dataType).Scale;
            }

            if (dataType is NumericDataType)
            {
                precision = ((NumericDataType)dataType).Precision;
                scale = ((NumericDataType)dataType).Scale;
            }

            return string.Format("DecimalType({0},{1})", precision, scale);
        }
    }
}
