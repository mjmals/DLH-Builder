using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactDataType : IDataType
    {
        [JsonIgnore]
        public Type BaseType => typeof(DataArtifact);

        public override string ToString()
        {
            return "Data Artifact";
        }
        public string FormattedName()
        {
            return ToString();
        }

    }
}
