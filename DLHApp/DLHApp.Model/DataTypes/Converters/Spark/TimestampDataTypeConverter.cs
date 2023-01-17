using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public class TimestampDataTypeConverter : SparkDataTypeConverter, IDataTypeConverter
    {
        public override string[] SourceTypeNames => new string[] { "TimestampType", "DateType" };

        public override Type[] DataTypes => this.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(ITimestampDataType))).ToArray();


        public override IDataType Import(string dataType)
        {
            if(dataType == "DateType()")
            {
                return new DateDataType();
            }

            return new TimestampDataType();
        }

        public override string Export(IDataType dataType)
        {
            if(dataType is DateDataType)
            {
                return "DateType()";
            }

            return "TimestampType()";
        }
    }
}
