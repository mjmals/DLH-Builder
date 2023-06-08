using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataTypes
{
    public interface IDataType
    {
        public string[] DisplayNames { get; }

        public string Name { get; }

        public string FormattedValue();

        public object DefaultValue { get; }
    }
}
