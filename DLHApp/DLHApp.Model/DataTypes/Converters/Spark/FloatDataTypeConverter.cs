using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class FloatDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "FloatType" };

        public override Type[] DataTypes => new Type[] { typeof(FloatDataType) };

        public override IDataType Import(string dataType)
        {
            return new FloatDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "FloatType()";
        }
    }
}
