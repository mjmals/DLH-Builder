using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactCollection : BuilderCollection<DataArtifact>
    {
        protected override string DirectoryName => string.Empty;

        [JsonIgnore]
        public EventHandler<DataArtifactEventArgs> ArtifactAdded;


        public new void Add(DataArtifact artifact)
        {
            base.Add(artifact);
            ArtifactAdded?.Invoke(this, new DataArtifactEventArgs(artifact));
        }
    }
}
