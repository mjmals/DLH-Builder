using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHApp.Model.DataTypes
{
    public abstract class DataType : IDataType
    {
        [JsonIgnore]
        public virtual string[] DisplayNames => new string[0];

        public virtual string Name => this.GetType().Name;

        public virtual string FormattedValue()
        {
            return string.Format("{0}()", DisplayNames[0]);
        }

        public override string ToString()
        {
            return FormattedValue();
        }
    }
}
