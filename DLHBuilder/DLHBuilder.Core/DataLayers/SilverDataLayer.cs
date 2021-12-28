using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class SilverDataLayer : DataLayer
    {
        public SilverDataLayer()
        {
            _type = DataLayerType.Silver;
            _path = "/STD/";
            _container = "root";
        }
    }
}
