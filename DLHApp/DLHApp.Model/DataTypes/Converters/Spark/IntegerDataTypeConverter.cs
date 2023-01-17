using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class IntegerDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "IntegerType" };

        public override Type[] DataTypes => this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IIntegerDataType))).ToArray();

        public override IDataType Import(string dataType)
        {
            return new IntegerDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "IntegerType()";
        }
    }
}
