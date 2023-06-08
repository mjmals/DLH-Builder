using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class NumericDataType : DataType, IDataType
    {
        public NumericDataType()
        {

        }

        public NumericDataType(string precision, string scale)
        {
            Precision = Convert.ToInt32(precision);
            Scale = Convert.ToInt32(scale);
        }

        public override string[] DisplayNames => new string[] { "Numeric", "NumericDataType", "NumericType" };

        public int Precision { get; set; }

        public int Scale { get; set; }

        public override string FormattedValue()
        {
            return string.Format("{0}({1},{2})", DisplayNames[0], Precision, Scale);
        }

        public override object DefaultValue => -1;
    }
}
