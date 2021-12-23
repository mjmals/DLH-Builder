using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DLHBuilder
{
    public class MetadataController
    {
        public static void SaveObject(object item, string path)
        {
            string data = JsonConvert.SerializeObject(item, Formatting.Indented);

            if(!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                stream.SetLength(0);

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }
    }
}
