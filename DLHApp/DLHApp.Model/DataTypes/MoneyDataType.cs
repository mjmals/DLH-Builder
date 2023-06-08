using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class MoneyDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Money", "MoneyDataType", "MoneyType" };

        public override object DefaultValue => -1;
    }
}
