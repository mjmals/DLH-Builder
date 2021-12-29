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

        public Dependency[] Dependencies
        {
            get => _dependencies.ToArray();
            set => _dependencies = ((Dependency[])value).ToList();
        }

        public void Add(string artifact, DataLayerType layer)
        {
            Dependency dependency = new Dependency();
            dependency.SourceArtifact = artifact;
            dependency.DataLayer = layer;

            _dependencies.Add(dependency);
        }
    }
}
