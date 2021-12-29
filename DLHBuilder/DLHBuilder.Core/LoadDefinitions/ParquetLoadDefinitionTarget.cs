using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ParquetLoadDefinitionTarget : LoadDefinitionTarget
    {
        public ParquetLoadDefinitionTarget(DataLayerType targetlayer) : base(targetlayer)
        {
            _type = DataFileFormat.Parquet;
        }

        public string DirectoryName { get; set; }

        public string Path { get; set; }

        public override string FullPath()
        {
            return string.Format("/{0}/{1}.parquet", Path, DirectoryName);
        }
    }
}
