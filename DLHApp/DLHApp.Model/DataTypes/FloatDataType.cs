using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class FloatDataType : DataType, IDataType
    {
        public FloatDataType()
        {

        }

        public FloatDataType(string precision)
        {
            Precision = Convert.ToInt32(precision);
        }

        public override string[] DisplayNames => new string[] { "Float", "FloatDataType", "FloatType" };

        public int Precision { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", DisplayNames[0], Precision);
        }

        public override object DefaultValue => new float();
    }
}
