using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class BooleanDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "bit" };

        public override Type[] DataTypes => new Type[] { typeof(BooleanDataType) };

        public override IDataType Import(string dataType)
        {
            return new BooleanDataType();
        }

        public override string Export(IDataType dataType)
        {
            if (!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a boolean data type");
            }

            return "bit";
        }

        public override string GetDefaultValue(IDataType dataType)
        {
            return "0";
        }
    }
}
