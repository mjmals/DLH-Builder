using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class LandingDataLayer : DataLayer
    {
        public LandingDataLayer()
        {
            _type = DataLayerType.Landing;
            _path = "/LND/";
            _container = "root";
        }
    }
}
