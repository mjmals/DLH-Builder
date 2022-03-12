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

        protected override string FileNameProperty => "FullName";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        [JsonIgnore]
        public EventHandler<DataArtifactEventArgs> ArtifactAdded;


        public new void Add(DataArtifact artifact)
        {
            base.Add(artifact);
            ArtifactAdded?.Invoke(this, new DataArtifactEventArgs(artifact));
        }

        internal override void Save(string path)
        {
            base.Save(path);

            foreach(DataArtifact artifact in this)
            {
                artifact.DataSources.Save(Path.Combine(path, DirectoryName, artifact.FullName));
                artifact.Schema.Save(Path.Combine(path, DirectoryName, artifact.FullName));
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            foreach(DataArtifact artifact in this)
            {
                artifact.DataSources.Load(Path.Combine(path, DirectoryName, artifact.FullName));
                artifact.Schema.Load(Path.Combine(path, DirectoryName, artifact.FullName));
            }
        }
    }
}
