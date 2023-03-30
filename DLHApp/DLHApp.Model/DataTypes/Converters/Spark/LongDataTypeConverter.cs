using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class LongDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "LongType" };

        public override Type[] DataTypes => new Type[] { typeof(LongDataType) };

        public override IDataType Import(string dataType)
        {
            return new LongDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "LongType()";
        }
    }
}
