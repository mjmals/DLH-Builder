using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class MoneyDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "money" };

        public override Type[] DataTypes => new Type[] { typeof(MoneyDataType) };

        public override IDataType Import(string dataType)
        {
            return new MoneyDataType();
        }

        public override string Export(IDataType dataType)
        {
            if(!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a money data type");
            }

            return "money";
        }
    }
}
