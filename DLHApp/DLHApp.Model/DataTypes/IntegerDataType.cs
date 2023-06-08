using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataTypes
{
    public class IntegerDataType : DataType, IIntegerDataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Int", "IntegerDataType", "IntegerType" };

        public override string FormattedValue()
        {
            return String.Format("{0}()",DisplayNames[0]);
        }

        public override object DefaultValue => -1;
    }
}
