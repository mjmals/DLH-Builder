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
        protected override string DirectoryName => "Data Artifacts";

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "ArtifactPath";

        protected override string FileSearchPattern => "*DataArtifact.json";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        [JsonIgnore]
        public EventHandler<DataArtifactEventArgs> ArtifactAdded;

        [JsonIgnore]
        public EventHandler<DataArtifactEventArgs> NewArtifactCreated;

        public new void Add(DataArtifact artifact)
        {
            base.Add(artifact);
            ArtifactAdded?.Invoke(this, new DataArtifactEventArgs(artifact));

            //if(artifact is DataArtifact)
            //{
            //    if(((DataArtifact)artifact).MasterDataArtifactID == Guid.Empty)
            //    {
            //        MasterDataArtifactHandler.PostArtifact((DataArtifact)artifact);
            //    }
            //}
        }

        internal override void Save(string path)
        {
            base.Save(path);

            //foreach(DataArtifact artifact in this)
            //{
            //    artifact.DataSources.Save(Path.Combine(path, DirectoryName, artifact.FullName.Replace(".", @"\")));
            //    artifact.Schema.Save(Path.Combine(path, DirectoryName, artifact.FullName.Replace(".", @"\")));
            //}
        }

        internal override void Load(string path)
        {
            base.Load(path);

            //foreach(DataArtifact artifact in this)
            //{
            //    artifact.DataSources.Load(Path.Combine(path, DirectoryName, artifact.FullName.Replace(".", @"\")));
            //    artifact.Schema.Load(Path.Combine(path, DirectoryName, artifact.FullName.Replace(".", @"\")));
            //}
        }
    }
}
