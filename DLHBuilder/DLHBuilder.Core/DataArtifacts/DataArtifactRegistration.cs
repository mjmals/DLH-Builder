using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataArtifacts
{
    public class DataArtifactRegistration
    {
        public DataArtifactRegistrationType Type { get; set; }

        public string Database { get; set; }

        public string Table { get; set; }
    }
}
