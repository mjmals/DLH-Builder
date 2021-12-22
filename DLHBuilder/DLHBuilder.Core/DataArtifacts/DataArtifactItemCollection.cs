using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifactItemCollection
    {
        public DataArtifactItemCollection()
        {

        }

        List<DataArtifactItem> _items = new List<DataArtifactItem>();

        public DataArtifactItem[] Items
        {
            get => _items.ToArray();
            set => _items = ((DataArtifactItem[])value).ToList();
        }

        public void Add(DataArtifactItem item)
        {
            _items.Add(item);
        }
    }
}
