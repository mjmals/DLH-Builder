using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataApplicationCollection : List<DataApplication>
    {
        const string DirectoryPath = "Data Applications";

        internal void Save(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (DataApplication application in this)
            {
                string filepath = Path.Combine(path, application.Name, string.Format("{0}.{1}.json", application.Name, application.GetType().Name));

                if (!Directory.Exists(Path.GetDirectoryName(filepath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                }

                FileMetadataExtractor extractor = new FileMetadataExtractor(filepath);
                extractor.Write(application);
            }
        }

        internal static DataApplicationCollection Load(string path)
        {
            path = Path.Combine(path, DirectoryPath);

            DataApplicationCollection output = new DataApplicationCollection();

            foreach (string folder in Directory.GetDirectories(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    string fileshort = Path.GetFileNameWithoutExtension(file);
                    int startpos = fileshort.IndexOf(".") + 1;

                    string filetype = string.Concat(typeof(DataApplicationCollection).Namespace, ".", fileshort.Substring(startpos));

                    Type type = Type.GetType(filetype);

                    FileMetadataExtractor extractor = new FileMetadataExtractor(file);
                    output.Add((DataApplication)extractor.LoadFile(type));
                }
            }

            return output;
        }
    }
}