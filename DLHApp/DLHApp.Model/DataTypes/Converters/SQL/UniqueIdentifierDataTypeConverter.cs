using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.SQL
{
    public class UniqueIdentifierDataTypeConverter : SqlDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "uniqueidentifier" };

        public override Type[] DataTypes => new Type[] { typeof(UniqueIdentifierDataType) };

        public override IDataType Import(string dataType)
        {
            return new UniqueIdentifierDataType();
        }

        public override string Export(IDataType dataType)
        {
            if(!DataTypes.Contains(dataType.GetType()))
            {
                throw new Exception("Specified data type is not a uniqueidentifier data type");
            }

            return "uniqueidentifier";
        }
    }
}
