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

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                new StreamWriter(fs).Write(data);
            }
        }
    }
}
