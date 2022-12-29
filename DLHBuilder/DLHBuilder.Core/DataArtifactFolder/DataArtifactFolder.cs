using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifactFolder
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public List<string> Path { get; set; }

        public string FullPath
        {
            get
            {
                List<string> output = new List<string>();
                output.AddRange(Path);
                output.Add(Name);
                return string.Join('.', output);
            }
        }
    }
}
