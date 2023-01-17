using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes.Converters.Spark
{
    public abstract class SparkDataTypeConverter : DataTypeConverter, IDataTypeConverter
    {
        public override DataTypeConverterType ConverterType => DataTypeConverterType.Spark;
    }
}
