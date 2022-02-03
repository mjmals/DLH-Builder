using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactSource
    {
        public Guid ConnectionID { get; set; }

        public string CommandType { get; set; }

        public string Command { get; set; }
    }
}
