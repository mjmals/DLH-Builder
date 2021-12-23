using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataArtifactPropertyCollection
    {
        public DataArtifactPropertyCollection()
        {

        }

        List<DataArtifactProperty> _items = new List<DataArtifactProperty>();

        public DataArtifactProperty[] Items
        {
            get => _items.ToArray();
            set => _items = ((DataArtifactProperty[])value).ToList();
        }

        public void Add(DataArtifactProperty item)
        {
            _items.Add(item);
        }
    }
}
