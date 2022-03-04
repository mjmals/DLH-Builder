using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactEventArgs : EventArgs
    {
        public DataArtifactEventArgs(DataArtifact artifact)
        {
            Artifact = artifact;
        }

        public DataArtifact Artifact { get; set; }
    }
}
