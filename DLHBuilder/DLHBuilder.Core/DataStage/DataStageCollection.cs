using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataStageCollection : BuilderCollection<IDataStage>
    {
        protected override string DirectoryName => "Data Stages";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        public new void Add(IDataStage stage)
        {
            base.Add(stage);
            stage.Ordinal = this.IndexOf(stage);
        }

        internal override void Save(string path)
        {
            base.Save(path);

            foreach(IDataStage stage in this)
            {
                stage.Artifacts.Save(Path.Combine(path, DirectoryName, stage.Name));
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            foreach (IDataStage stage in this)
            {
                stage.Artifacts.Load(Path.Combine(path, DirectoryName, stage.Name));
            }
        }
    }
}
