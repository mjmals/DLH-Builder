using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class FloatDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "float" };

        public override Type[] DataTypes => new Type[] { typeof(FloatDataType) };

        public override IDataType Import(string dataType)
        {
            FloatDataType output = new FloatDataType();

            dataType = dataType.ToLower();

            if (dataType.Contains("(") && dataType.Contains(")"))
            {
                int parmStart = dataType.IndexOf("(");
                int parmEnd = dataType.IndexOf(")");
                string param = dataType.Substring(parmStart + 1, parmEnd - parmStart - 1);

                output.Precision = Convert.ToInt32(param);
            }

            return output;
        }

        public override string Export(IDataType dataType)
        {
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a float data type");
            }

            FloatDataType decimalType = (FloatDataType)dataType;
            return string.Format("float({0})", decimalType.Precision);
        }
    }
}
