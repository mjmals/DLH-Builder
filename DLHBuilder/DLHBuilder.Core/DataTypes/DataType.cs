using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataTypes
{
    public abstract class DataType : IDataType
    {
        public virtual IDataType ConvertTo(string type, DataItemFormat format)
        {
            return null;
        }

        public virtual string ConvertFrom(DataItemFormat format)
        {
            return null;
        }
    }
}
