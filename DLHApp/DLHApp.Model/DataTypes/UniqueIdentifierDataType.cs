using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public class UniqueIdentifierDataType : DataType, IDataType
    {
        public override string[] DisplayNames => new string[] { "Uid", "UidDataType", "UidType", "UniqueIdentifier", "UniqueIdentiferDataType", "UniqueIdentifierType" };

        public override object DefaultValue => Guid.Empty;
    }
}
