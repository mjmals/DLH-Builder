using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifact
    {
        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated()
        {
            PropertyUpdated?.Invoke(null, null);
        }

        public Guid ID { get; set; }

        //public Guid MasterDataArtifactID { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyUpdated();
            }
        }

        private string name { get; set; }

        public List<string> ArtifactNamespace
        {
            get
            {
                if(artifactnamespace == null)
                {
                    artifactnamespace = new List<string>();
                }
                return artifactnamespace;
            }
            set
            {
                OnPropertyUpdated();
                artifactnamespace = value;
            }
        }

        private List<string> artifactnamespace { get; set; }

        [JsonIgnore]
        public string ArtifactPath => string.Join('.', ArtifactNamespace);

        [JsonIgnore]
        public string FullName => ArtifactNamespace.Count == 0 ? Name : string.Format("{0}.{1}", ArtifactPath, Name);

        public string Description { get; set; }

        //[JsonIgnore]
        public DataSourceCollection DataSources
        {
            get
            {
                if(datasources == null)
                {
                    datasources = new DataSourceCollection();
                }
                return datasources;
            }
            set => datasources = value;
        }

        private DataSourceCollection datasources { get; set; }

        //[JsonIgnore]
        public DataArtifactSchemaItemCollection Schema
        { 
            get
            {
                if(schema == null)
                {
                    schema = new DataArtifactSchemaItemCollection();
                    schema.SchemaItemAdded += OnSchemaItemAdded;
                }
                return schema;
            }
            set
            {
                schema = value;
                schema.SchemaItemAdded += OnSchemaItemAdded;
            }
        }

        private DataArtifactSchemaItemCollection schema { get; set; }

        public DataArtifactSchemaItem[] ListPrimaryKeys() 
        {
            DataArtifactSchemaItem[] schemaitems;//= new []DataArtifactSchemaItem();

            schemaitems = this.Schema
                .Where(e => e.KeyType == DataArtifactSchemaItemKeyType.Primary)
                .ToArray();
            return schemaitems;
        }

        public DataArtifactSchemaItem[] ListBusinessKeys()
        {
            DataArtifactSchemaItem[] schemaitems;//= new []DataArtifactSchemaItem();

            schemaitems = this.Schema
                .Where(e => e.KeyType == DataArtifactSchemaItemKeyType.Business)
                .ToArray();
            return schemaitems;
        }

        public DataArtifactSchemaItem[] ListVersionColumns()
        {
            DataArtifactSchemaItem[] schemaitems;//= new []DataArtifactSchemaItem();

            schemaitems = this.Schema
                .Where(e => e.KeyType == DataArtifactSchemaItemKeyType.Version)
                .ToArray();
            return schemaitems;
        }

        private void OnSchemaItemAdded(object sender, DataArtifactSchemaItemEventArgs e)
        {
            //MasterDataArtifactHandler.PostSchemaItem(MasterDataArtifactID, e.Item);
        }

        public DataArtifactTransformationCollection Transformations
        {
            get
            {
                if(transformations == null)
                {
                    transformations = new DataArtifactTransformationCollection();
                }
                return transformations;
            }
            set => transformations = value;
        }

        private DataArtifactTransformationCollection transformations { get; set; }

        public ScriptTemplateReferenceCollection ScriptTemplates
        {
            get
            {
                if(scripttemplates == null)
                {
                    scripttemplates = new ScriptTemplateReferenceCollection() { Type = ScriptTemplateReferenceType.Inherited };
                }
                return scripttemplates;
            }
            set => scripttemplates = value;
        }

        private ScriptTemplateReferenceCollection scripttemplates { get; set; }

        public static DataArtifact New(string path = null)
        {
            DataArtifact output = new DataArtifact();
            output.ID = Guid.NewGuid();
            output.Name = "<New Artifact>";

            if (!string.IsNullOrEmpty(path))
            {
                output.ArtifactNamespace.AddRange(path.Split('.'));
            }

            return output;
        }
    }
}
