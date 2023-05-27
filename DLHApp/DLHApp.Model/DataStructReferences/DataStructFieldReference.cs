using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructReferences
{
    public class DataStructFieldReference
    {
        public string SourceField { get; set; }

        public string OutputName { get; set; }

        public DataStructFieldReferenceMetadataCollection Metadata { get; set; }
    }
}
