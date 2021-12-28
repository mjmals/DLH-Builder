using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DeltaLoadDefinitionTarget : LoadDefinitionTarget
    {
        public DeltaLoadDefinitionTarget(DataLayer targetlayer) : base(targetlayer)
        {
            _type = DataFileFormat.Delta;
        }

        public string DirectoryName { get; set; }

        public string Path { get; set; }

        public override string FullPath()
        {
            return string.Format("/{0}/{1}", Path, DirectoryName);
        }
    }
}
