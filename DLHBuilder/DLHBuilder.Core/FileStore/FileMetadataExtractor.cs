using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    class FileMetadataExtractor
    {
        public FileMetadataExtractor(string file)
        {
            FilePath = file;
        }

        string FilePath { get; }

        JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public void Write(object item)
        {
            string itemdata = JsonConvert.SerializeObject(item, Formatting.Indented, SerializerSettings);
            Write(itemdata);
        }

        public void Write(string text)
        {
            using (FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                }
            }
        }

        public T LoadFile<T>()
        {
            string filedata = File.ReadAllText(FilePath);

            return JsonConvert.DeserializeObject<T>(filedata, SerializerSettings);
        }

        public object LoadFile(Type type)
        {
            string filedata = File.ReadAllText(FilePath);

            return JsonConvert.DeserializeObject(filedata, type, SerializerSettings);
        }
    }
}
