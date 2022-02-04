using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataConnectionCollection : List<DataConnection>
    {
        public void Save(string path)
        {
            path = Path.Combine(path, "Connections");

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach(DataConnection connection in this)
            {
                string filename = string.Format("{0}.{1}.json", connection.Name, connection.GetType().Name);
                string filepath = Path.Combine(path, filename);

                FileMetadataExtractor extractor = new FileMetadataExtractor(filepath);
                extractor.Write(connection);
            }
        }

        public static DataConnectionCollection Load(string path)
        {
            DataConnectionCollection output = new DataConnectionCollection();

            path = Path.Combine(path, "Connections");

            foreach(string file in Directory.GetFiles(path))
            {
                string fileshort = Path.GetFileNameWithoutExtension(file);
                int startpos = fileshort.IndexOf(".") + 1;

                string filetype = string.Concat(typeof(DataConnectionCollection).Namespace, ".", fileshort.Substring(startpos));

                Type type = Type.GetType(filetype);

                FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                output.Add((DataConnection)extractor.LoadFile(type));
            }

            return output;
        }
    }
}
