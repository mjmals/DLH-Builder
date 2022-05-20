using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactDependencyCollection : BuilderCollection<IDataArtifactDependency>
    {
        protected override string FileSearchPattern => "*Dependency.json";

        protected override string FileNameProperty => "Name";

        protected override string DirectoryName => "Dependencies";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;
    }
}
