using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    internal class BinaryDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "binary", "varbinary", "timestamp", "rowversion" };

        public override Type[] DataTypes => new Type[] { typeof(BinaryDataType) };

        public override IDataType Import(string dataType)
        {
            BinaryDataType output = new BinaryDataType();

            dataType = dataType.ToLower();

            switch(dataType)
            {
                case "timestamp":
                    output.Length = 8;
                    break;
                case "rowversion":
                    output.Length = 8;
                    break;
                default:
                    int parmStart = dataType.IndexOf("(");
                    int parmEnd = dataType.IndexOf(")");
                    string param = dataType.Substring(parmStart + 1, parmEnd - parmStart - 1);
                    output.Length = Convert.ToInt32(param == "MAX" ? -1 : param);
                    break;
            }

            return output;
        }

        public override string Export(IDataType dataType)
        {
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a string data type");
            }

            string output = string.Empty;

            BinaryDataType binaryType = (BinaryDataType)dataType;

            output += string.Format("varchar({0})", binaryType.Length <= 0 ? "MAX" : binaryType.Length.ToString());

            return output;
        }
    }
}
