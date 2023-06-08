using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class LongDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Long", "LongDataType", "LongType" };

        public override object DefaultValue => -1;
    }
}
