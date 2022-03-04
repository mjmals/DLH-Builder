using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactGroupEventArgs : EventArgs
    {
        public DataArtifactGroupEventArgs(DataArtifactGroup group)
        {
            Group = group;
        }

        public DataArtifactGroup Group { get; set; }
    }
}
