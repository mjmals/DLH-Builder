using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class IntegerDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "int", "bigint", "smallint", "tinyint" };

        public override Type[] DataTypes => new Type[] { typeof(IntegerDataType), typeof(BigIntegerDataType), typeof(SmallIntegerDataType), typeof(TinyIntegerDataType) };

        public override IDataType Import(string dataType)
        {
            switch(dataType.ToLower())
            {
                case "bigint":
                    return new BigIntegerDataType();
                case "smallint":
                    return new SmallIntegerDataType();
                case "tinyint":
                    return new TinyIntegerDataType();
                default:
                    return new IntegerDataType();
            }
        }

        public override string Export(IDataType dataType)
        {
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not an integer data type");
            }

            if(dataType.GetType() == typeof(BigIntegerDataType))
            {
                return "bigint";
            }

            if(dataType.GetType() == typeof(SmallIntegerDataType))
            {
                return "smallint";
            }

            if(dataType.GetType() == typeof(TinyIntegerDataType))
            {
                return "tinyint";
            }

            return "int";
        }
    }
}
