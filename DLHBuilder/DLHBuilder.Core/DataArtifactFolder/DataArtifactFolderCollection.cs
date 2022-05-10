using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifactFolderCollection : BuilderCollection<DataArtifactFolder>
    {
        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "FullPath";

        protected override string DirectoryName => "Data Artifacts";

        protected override string FileSearchPattern => "*DataArtifactFolder.json";
    }
}
