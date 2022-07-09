using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataType
    {
        Type BaseType { get; }

        string FormattedName();
    }
}
