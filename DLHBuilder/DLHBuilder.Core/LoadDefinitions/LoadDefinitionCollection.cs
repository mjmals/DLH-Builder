using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class LoadDefinitionCollection
    {
        public LoadDefinitionCollection()
        {

        }

        List<LoadDefinition> _definitions = new List<LoadDefinition>();

        public LoadDefinition[] Definitions
        {
            get => _definitions.ToArray();
            set => _definitions = ((LoadDefinition[])value).ToList();
        }

        public void Add(LoadDefinition definition)
        {
            _definitions.Add(definition);
        }
    }
}
