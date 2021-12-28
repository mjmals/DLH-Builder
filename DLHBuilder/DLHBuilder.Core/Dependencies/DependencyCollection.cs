using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DependencyCollection
    {
        public DependencyCollection()
        {

        }

        List<Dependency> _dependencies = new List<Dependency>();

        Dependency[] Dependencies
        {
            get => _dependencies.ToArray();
            set => _dependencies = ((Dependency[])value).ToList();
        }
    }
}
