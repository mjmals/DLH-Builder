using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructs
{
    public class DataStructRelationship
    {
        public string SourceDataStruct { get; set; }

        public string OutputField { get; set; }

        public DataStructRelationshipJoinCollection Joins { get; set; }
    }
}
