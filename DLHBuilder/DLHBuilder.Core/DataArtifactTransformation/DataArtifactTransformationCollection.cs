using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactTransformationCollection : BuilderCollection<IDataArtifactTransformation>
    {
        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        protected override string FileNameProperty => "Name";

        protected override string FileSearchPattern => "*DataArtifactTransformation.json";

        protected override string DirectoryName => "Transformations";
    }
}
