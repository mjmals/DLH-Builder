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
        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "FullPath";

        protected override string DirectoryName => "Data Artifacts";

        protected override string FileSearchPattern => "DataArtifactFolders.json";

        internal override void Save(string path)
        {
            string directoryName = Path.Combine(path, DirectoryName);

            if(!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            FileMetadataExtractor extractor = new FileMetadataExtractor(Path.Combine(directoryName, "DataArtifactFolders.json"));
            extractor.Write(this);
        }

        internal override void Load(string path)
        {
            FileMetadataExtractor extractor = new FileMetadataExtractor(Path.Combine(path, DirectoryName, "DataArtifactFolders.json"));
            AddRange(extractor.LoadFile<DataArtifactFolderCollection>());
        }
    }
}
