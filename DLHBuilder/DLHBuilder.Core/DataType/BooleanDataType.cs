using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder.DataType
{
    public class BooleanDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(bool);

        public override string ToString()
        {
            return "Boolean";
        }
        public string FormattedName()
        { 
            return ToString();
        }
    }
}
