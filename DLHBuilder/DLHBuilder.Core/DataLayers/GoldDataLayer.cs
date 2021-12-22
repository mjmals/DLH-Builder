using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class GoldDataLayer : DataLayer
    {
        public GoldDataLayer()
        {
            _type = DataLayerType.Gold;
            _path = "/CUR/";
            _container = "root";
        }
    }
}
