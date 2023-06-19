using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructs
{
    public class DataStructRelationshipJoin
    {
        public string SourceField { get; set; }

        public string TargetField { get; set; }

        public bool IsCaseSensitive { get; set; }

        public override string ToString()
        {
            if(string.IsNullOrEmpty(SourceField) && string.IsNullOrEmpty(TargetField))
            {
                return base.ToString();
            }

            return string.Format("{0} -> {1}", SourceField, TargetField);
        }
    }
}
