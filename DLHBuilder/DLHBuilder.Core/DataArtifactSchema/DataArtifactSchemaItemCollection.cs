using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifactSchemaItemCollection : BuilderCollection<DataArtifactSchemaItem>
    {
        protected override string DirectoryName => "Schema";

        protected override string FileNameProperty => "Name";

        protected override string SubfolderProperty => "Name";

        protected override string FileSearchPattern => "*DataArtifactSchemaItem.json";

        protected override BuilderCollectionItemType CollectionType => BuilderCollectionItemType.FolderAndFile;

        [JsonIgnore]
        public EventHandler<DataArtifactSchemaItemEventArgs> SchemaItemAdded;

        public new void Add(DataArtifactSchemaItem item)
        {
            base.Add(item);
            SchemaItemAdded?.Invoke(this, new DataArtifactSchemaItemEventArgs(item));
        }

        internal override void Save(string path)
        {
            base.Save(path);

            foreach(DataArtifactSchemaItem schemaItem in this)
            {
                string dtPath = Path.Combine(path, DirectoryName, schemaItem.Name, schemaItem.Name + "." + schemaItem.DataType.GetType().Name + ".json");

                FileMetadataExtractor extractor = new FileMetadataExtractor(dtPath);
                extractor.Write(schemaItem.DataType);
            }
        }

        internal override void Load(string path)
        {
            base.Load(path);

            foreach(DataArtifactSchemaItem schemaItem in this)
            {
                string dtFile = Directory.GetFiles(Path.Combine(path, DirectoryName, schemaItem.Name), "*DataType.json").FirstOrDefault();
                string dtFileType = dtFile.Replace(Path.Combine(path, DirectoryName, schemaItem.Name) + @"\", "").Replace(schemaItem.Name + ".", "").Replace(".json", "");

                Type dtType = schemaItem.GetType().Assembly.GetTypes().FirstOrDefault(x => x.Name == dtFileType);
                FileMetadataExtractor extractor = new FileMetadataExtractor(dtFile);
                
                schemaItem.DataType = (IDataType)extractor.LoadFile(dtType);
            }
        }
    }
}
