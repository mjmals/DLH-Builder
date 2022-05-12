using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactReferenceCollection : BuilderCollection<DataArtifactReference>
    {
        protected override string DirectoryName => "Data Artifact References";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        protected override string FileSearchPattern => "*DataArtifactReference.json";
    }
}
