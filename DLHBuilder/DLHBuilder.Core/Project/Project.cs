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


        //[JsonIgnore]
        //private DataArtifactCollection MasterArtifacts
        //{
        //    get
        //    {
        //        if(MasterDataArtifactHandler.Artifacts == null)
        //        {
        //            MasterDataArtifactHandler.Artifacts = new DataArtifactCollection();
        //        }
        //        return MasterDataArtifactHandler.Artifacts;
        //    }
        //    set => MasterDataArtifactHandler.Artifacts = value;
        //}


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
            //MasterArtifacts.Save(path);
        }

        public static Project Load(string file)
        {
            string path = Path.GetDirectoryName(file);

            Project output = new FileMetadataExtractor(file).LoadFile<Project>();
            output.Connections.Load(path);
            output.Applications.Load(path);
            //output.MasterArtifacts.Load(path);

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
