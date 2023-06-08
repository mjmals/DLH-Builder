using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    /// <summary>
    /// all
    /// </summary>
    public class SmallMoneyDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "SmallMoney", "SmallMoneyDataType", "SmallMoneyType" };

        public override object DefaultValue => -1;
    }
}
