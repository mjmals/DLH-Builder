using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataStageFolderCollection : BuilderCollection<DataStageFolder>
    {
        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "FullPath";

        protected override string FileSearchPattern => "*DataStageFolder.json";
    }
}
