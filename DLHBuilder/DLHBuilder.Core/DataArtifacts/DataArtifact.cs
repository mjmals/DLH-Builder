using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifact : IDataArtifact
    {
        public string Name { get; set; }

        [JsonIgnore]
        public DataArtifactPropertyCollection Properties { get; set; }

        [JsonIgnore]
        public LoadDefinitionCollection LoadDefinitions { get; set; }

        public LoadDefinition CreateLoadDefinition()
        {
            LoadDefinition output = new LoadDefinition();

            if (LoadDefinitions == null)
            {
                LoadDefinitions = new LoadDefinitionCollection();
            }

            LoadDefinitions.Add(output);

            return output;
        }

        internal void Save(string path)
        {
            path = Path.Combine(path, Name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = Path.Combine(path, "DataArtifact.json");

            MetadataController.SaveObject(this, filepath);

            string definitionpath = Path.Combine(path, "Load Definitions");

            if(!Directory.Exists(definitionpath))
            {
                Directory.CreateDirectory(definitionpath);
            }

            foreach(LoadDefinition definition in LoadDefinitions.Definitions)
            {

            }

            string propertypath = Path.Combine(path, "Properties");

            if (!Directory.Exists(propertypath))
            {
                Directory.CreateDirectory(propertypath);
            }

            foreach(DataArtifactProperty property in Properties.Items)
            {

            }
        }
    }
}
