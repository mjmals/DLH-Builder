using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifactReferenceCollection : BuilderCollection<DataArtifactReference>
    {
        protected override string DirectoryName => string.Empty;

        protected override string FileNameProperty => "Name";

        //protected override string SubfolderProperty => "FullPath";

        //protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.File;

        protected override string FileSearchPattern => "*DataArtifactReference.json";

        internal override void Save(string path)
        {
            base.Save(path);

            foreach(DataArtifactReference reference in this)
            {
                string referencePath = Path.Combine(path, DirectoryName, reference.FullPath.Replace(".", @"\"));
                //reference.Transformations.Save(referencePath);
                //reference.Dependencies.Save(referencePath);
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            /*foreach (DataArtifactReference reference in this)
            {
                string referencePath = Path.Combine(path, DirectoryName, reference.FullPath.Replace(".", @"\"));
                reference.Dependencies.Load(referencePath);

                foreach(string file in Directory.GetFiles(Path.Combine(referencePath, "Transformations"), "*DataArtifactTransformation.json"))
                {
                    string typeName = Path.GetFileNameWithoutExtension(file).Split('.').LastOrDefault();
                    Type type = this.GetType().Assembly.GetTypes().FirstOrDefault(x => x.Name == typeName);

                    FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                    reference.Transformations.Add((IDataArtifactTransformation)extractor.LoadFile(type));
                }
            }*/
        }
    }
}
