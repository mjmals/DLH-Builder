using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class BronzeDataLayer : DataLayer
    {
        public BronzeDataLayer()
        {
            _type = DataLayerType.Bronze;
            _path = "/RAW/";
            _container = "root";
        }
    }
}
