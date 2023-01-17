using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class BooleanDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "BooleanType" };

        public override Type[] DataTypes => new Type[] { typeof(BooleanDataType) };

        public override IDataType Import(string dataType)
        {
            return new BooleanDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "BooleanType()";
        }
    }
}
