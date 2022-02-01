using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataStageCollection : List<DataStage>
    {
        internal void Save(string path)
        {
            path = Path.Combine(path, "Data Stages");

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach(DataStage stage in this)
            {
                string filepath = Path.Combine(path, stage.Name, string.Format("{0}.json", stage.Name));

                if(!Directory.Exists(Path.GetDirectoryName(filepath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                }

                FileMetadataExtractor extractor = new FileMetadataExtractor(filepath);
                extractor.Write(stage);
            }
        }

        internal static DataStageCollection Load(string path)
        {
            path = Path.Combine(path, "Data Stages");

            DataStageCollection output = new DataStageCollection();

            foreach(string folder in Directory.GetDirectories(path))
            {
                DirectoryInfo directory = new DirectoryInfo(folder);
                string file = Path.Combine(folder, string.Format("{0}.json", directory.Name));

                DataStage stage = new FileMetadataExtractor(file).LoadFile<DataStage>();
                output.Add(stage);
            }

            return output;
        }
    }
}
