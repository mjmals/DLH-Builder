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

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "Name";

        protected override string FileSearchPattern => "*DataStage.json";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        public new void Add(IDataStage stage)
        {
            base.Add(stage);
            stage.Ordinal = this.IndexOf(stage);
        }

        internal override void Save(string path)
        {
            base.Save(path);
        }

        internal override void Load(string path)
        {
            base.Load(path);
        }
    }
}
