using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class StringDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "StringType" };

        public override Type[] DataTypes => this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IStringDataType))).ToArray();

        public override IDataType Import(string dataType)
        {
            return new StringDataType();
        }

        public override string Export(IDataType dataType)
        {
            return "StringType()";
        }
    }
}
