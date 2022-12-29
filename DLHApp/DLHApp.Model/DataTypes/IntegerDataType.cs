using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class IntegerDataType : IIntegerDataType, IDataType
    {
        public string[] DisplayNames => new string[] { "Int", "IntegerDataType", "IntegerType" };

        public string FormattedValue()
        {
            return String.Format("{0}()",DisplayNames[0]);
        }
    }
}
