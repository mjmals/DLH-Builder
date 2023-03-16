using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructs
{
    public class DataStructRelationship
    {
        public DataStructRelationship()
        {
            Joins = new DataStructRelationshipJoinCollection();
        }

        public DataStructRelationship(string relText)
        {
            DataStructRelationshipParser parser = new DataStructRelationshipParser(relText, this);
            parser.Parse();
        }

        public string SourceDataStruct { get; set; }

        public string OutputField { get; set; }

        public DataStructRelationshipJoinCollection Joins { get; set; }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(SourceDataStruct) ? SourceDataStruct : base.ToString();
        }
    }
}
