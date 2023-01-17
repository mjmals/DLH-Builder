using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class BinaryDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "BinaryType" };

        public override Type[] DataTypes => new Type[] { typeof(BinaryDataType) };

        public override IDataType Import(string dataType)
        {
            return new BinaryDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "BinaryType()";
        }
    }
}
