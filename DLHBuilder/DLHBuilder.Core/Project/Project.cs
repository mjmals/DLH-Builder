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
        public string Name 
        { 
            get => name;
            set
            {
                name = value;
                OnPropertyUpdated(Name);
            }
        }

        private string name { get; set; }

        public string Description 
        { 
            get => description;
            set
            {
                description = value;
                OnPropertyUpdated(Description);
            }
        }

        private string description { get; set; }

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
        
        [JsonIgnore]
        public DataArtifactFolderCollection ArtifactFolders
        {
            get
            {
                if(artifactFolders == null)
                {
                    artifactFolders = new DataArtifactFolderCollection();
                }
                return artifactFolders;
            }
            set => artifactFolders = value;
        }

        private DataArtifactFolderCollection artifactFolders { get; set; }

        [JsonIgnore]
        public DataArtifactCollection Artifacts
        {
            get
            {
                if(artifacts == null)
                {
                    artifacts = new DataArtifactCollection();
                }
                return artifacts;
            }
            set => artifacts = value;
        }

        private DataArtifactCollection artifacts { get; set; }

        [JsonIgnore]
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


        [JsonIgnore]
        public ScriptTemplateCollection ScriptTemplates
        {
            get
            {
                if(scriptTemplates == null)
                {
                    scriptTemplates = new ScriptTemplateCollection();
                }
                return scriptTemplates;
            }
            set => scriptTemplates = value;
        }

        private ScriptTemplateCollection scriptTemplates { get; set; }

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
            ArtifactFolders.Save(path);
            Artifacts.Save(path);
            Applications.Save(path);
            ScriptTemplates.Save(path);
        }

        public static Project Load(string file)
        {
            string path = Path.GetDirectoryName(file);

            Project output = new FileMetadataExtractor(file).LoadFile<Project>();
            output.Connections.Load(path);
            output.ArtifactFolders.Load(path);
            output.Artifacts.Load(path);
            output.Applications.Load(path);
            output.ScriptTemplates.Load(path);

            return output;
        }

        [JsonIgnore]
        public EventHandler PropertyUpdated;

        void OnPropertyUpdated(object sender)
        {
            PropertyUpdated?.Invoke(sender, null);
        }
    }
}
