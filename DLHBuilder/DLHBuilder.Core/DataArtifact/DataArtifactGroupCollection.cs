using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactGroupCollection : BuilderCollection<DataArtifactGroup>
    {
        protected override string DirectoryName => "Data Artifacts";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.Folder;

        [JsonIgnore]
        public EventHandler<DataArtifactGroupEventArgs> GroupAdded;

        public new void Add(DataArtifactGroup group)
        {
            base.Add(group);
            GroupAdded?.Invoke(this, new DataArtifactGroupEventArgs(group));
        }

        internal override void Save(string path)
        {
            base.Save(path);
            
            foreach(DataArtifactGroup group in this)
            {
                group.Artifacts.Save(Path.Combine(path, DirectoryName, group.Name));
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            foreach(DataArtifactGroup group in this)
            {
                group.Artifacts = new DataArtifactCollection();
                group.Artifacts.Load(Path.Combine(path, DirectoryName, group.Name));
            }
        }
    }
}
