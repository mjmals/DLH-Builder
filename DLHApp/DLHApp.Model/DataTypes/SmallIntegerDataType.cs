using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class SmallIntegerDataType : IIntegerDataType, IDataType
    {
        public string[] DisplayNames => new string[] { "SmallInt", "SmallIntegerDataType", "SmallIntegerType" };

        public string FormattedValue()
        {
            return String.Format("{0}()", DisplayNames[0]);
        }
    }
}
