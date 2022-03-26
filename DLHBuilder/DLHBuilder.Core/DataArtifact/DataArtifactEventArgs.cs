using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactEventArgs : EventArgs
    {
        public DataArtifactEventArgs(IDataArtifact artifact)
        {
            Artifact = artifact;
        }

        public IDataArtifact Artifact { get; set; }
    }
}
