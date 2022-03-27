using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactSchemaItemCollection : BuilderCollection<DataArtifactSchemaItem>
    {
        protected override string DirectoryName => "Schema";

        [JsonIgnore]
        public EventHandler<DataArtifactSchemaItemEventArgs> SchemaItemAdded;

        public new void Add(DataArtifactSchemaItem item)
        {
            base.Add(item);
            SchemaItemAdded?.Invoke(this, new DataArtifactSchemaItemEventArgs(item));
        }
    }
}
