using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class StringDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(string);

        public int Length { get; set; }

        public bool IsUnicode { get; set; }

        public bool IsCaseSensitive { get; set; }

        public bool IsAccentSensitive { get; set; }

        public bool IsUniqueIdentifier { get; set; }

        public override string ToString()
        {
            return IsUniqueIdentifier ? "String (UniqueIdentifier)" : "String";
        }
        public string FormattedName()
        {
            return ToString();
        }

    }
}
