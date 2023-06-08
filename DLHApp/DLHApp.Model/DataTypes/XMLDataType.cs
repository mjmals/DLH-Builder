using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class XMLDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "XML", "XMLDataType", "XMLType" };


        public override string FormattedValue()
        {
            return base.FormattedValue();
        }

        public override object DefaultValue => string.Empty;
    }
}
