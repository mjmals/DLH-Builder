﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class DataArtifactCollection : List<DataArtifact>
    {
        const string DirectoryPath = "Data Artifacts";

        [JsonIgnore]
        public EventHandler<DataArtifactEventArgs> ArtifactAdded;


        public new void Add(DataArtifact artifact)
        {
            base.Add(artifact);
            ArtifactAdded?.Invoke(this, new DataArtifactEventArgs(artifact));
        }

        internal void Save(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (DataArtifact artifact in this)
            {
                string filepath = Path.Combine(path, artifact.Name, string.Format("{0}.json", artifact.Name));

                if (!Directory.Exists(Path.GetDirectoryName(filepath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                }

                FileMetadataExtractor extractor = new FileMetadataExtractor(filepath);
                extractor.Write(artifact);
            }
        }

        internal static DataArtifactCollection Load(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            return null;
        }
    }
}
