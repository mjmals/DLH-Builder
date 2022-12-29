using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public abstract class DefinitionCollection<T> : BuilderCollection<T> where T : IDefinition
    {
        internal override void Save(string path)
        {
            
        }

        internal override void Load(string path)
        {

        }
    }
}
