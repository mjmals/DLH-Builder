using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactCollection
    {
        public DataArtifactCollection()
        {

        }

        List<DataArtifact> _artifacts = new List<DataArtifact>();

        public DataArtifact[] Artifacts
        {
            get
            {
                return _artifacts.ToArray();
            }
            set
            {
                _artifacts = ((DataArtifact[])value).ToList();
            }
        }

        public string Name { get; set; }
    }
}
