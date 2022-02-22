using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel;

namespace DLHBuilder
{
    public class Project
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public DataConnectionCollection Connections
        {
            get
            {
                if(connections == null)
                {
                    connections = new DataConnectionCollection();
                }

                return connections;
            }
            set => connections = value;
        } 

        private DataConnectionCollection connections { get; set; }

        public DataArtifactGroupCollection ArtifactGroups
        {
            get
            {
                if(artifactgroups == null)
                {
                    artifactgroups = new DataArtifactGroupCollection();
                }

                return artifactgroups;
            }
            set => artifactgroups = value;
        }

        private DataArtifactGroupCollection artifactgroups { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public DataApplicationCollection Applications
        {
            get
            {
                if(applications == null)
                {
                    applications = new DataApplicationCollection();
                }

                return applications;
            }
            set => applications = value;
        }

        private DataApplicationCollection applications { get; set; }

        public void Save(string path)
        {
            path = Path.Combine(path, Name);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = Path.Combine(path, string.Format("{0}.project.json", Name));
            FileMetadataExtractor extractor = new FileMetadataExtractor(file);
            extractor.Write(this);

            Connections.Save(path);
            Applications.Save(path);
        }

        public static Project Load(string file)
        {
            string path = Path.GetDirectoryName(file);

            Project output = new FileMetadataExtractor(file).LoadFile<Project>();
            output.Connections = DataConnectionCollection.Load(path);
            output.Applications = DataApplicationCollection.Load(path);

            return output;
        }
    }
}
