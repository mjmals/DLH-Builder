using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class DataLayer
    {
        public DataLayerType Type { get => _type; }

        protected DataLayerType _type { get; set; }

        public string Container { get => _container; }

        protected string _container { get; set; }

        public string Path { get => _path; }

        protected string _path { get; set; }
    }
}
