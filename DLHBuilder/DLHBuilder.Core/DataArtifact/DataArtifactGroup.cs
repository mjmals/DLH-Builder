﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactGroup
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

        internal void Save(string path)
        {
            path = Path.Combine(path, Name);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        [JsonIgnore]
        public EventHandler<DataArtifactGroupEventArgs> PropertyUpdated;

        void OnPropertyUpdated(object property)
        {
            PropertyUpdated?.Invoke(property, new DataArtifactGroupEventArgs(this));
        }

        public static DataArtifactGroup New()
        {
            DataArtifactGroup output = new DataArtifactGroup();
            output.Name = "<New Group>";

            return output;
        }
    }
}