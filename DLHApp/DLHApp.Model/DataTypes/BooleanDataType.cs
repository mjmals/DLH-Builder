using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class BooleanDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Boolean", "BooleanDataType", "BooleanType" };

        public override object DefaultValue => false;
    }
}
