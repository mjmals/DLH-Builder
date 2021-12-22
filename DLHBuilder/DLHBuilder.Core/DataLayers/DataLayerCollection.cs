using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.DataLayers
{
    public class DataLayerCollection
    {
        public DataLayerCollection()
        {
            _datalayer.Add(new GoldDataLayer());
        }

        List<DataLayer> _datalayer = new List<DataLayer>();

        DataLayer[] DataLayers
        {
            get => _datalayer.ToArray();
            set => _datalayer = ((DataLayer[])value).ToList();
        }

        public DataLayer Find(DataLayerType type)
        {
            return _datalayer.FirstOrDefault(x => x.Type == type);
        }
    }
}
