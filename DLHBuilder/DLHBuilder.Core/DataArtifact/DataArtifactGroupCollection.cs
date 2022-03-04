﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactGroupCollection : List<DataArtifactGroup>
    {
        const string DirectoryPath = "Data Artifacts";

        [JsonIgnore]
        public EventHandler<DataArtifactGroupEventArgs> GroupAdded;

        public new void Add(DataArtifactGroup group)
        {
            base.Add(group);
            GroupAdded?.Invoke(this, new DataArtifactGroupEventArgs(group));
        }

        internal void Save(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (DataArtifactGroup group in this)
            {
                string grouppath = Path.Combine(path, group.Name);

                if (!Directory.Exists(grouppath))
                {
                    Directory.CreateDirectory(grouppath);
                }

                group.Save(grouppath);
            }
        }

        internal static DataArtifactGroupCollection Load(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            DataArtifactGroupCollection output = new DataArtifactGroupCollection();

            foreach (string folder in Directory.GetDirectories(path))
            {

            }

            return output;
        }
    }
}