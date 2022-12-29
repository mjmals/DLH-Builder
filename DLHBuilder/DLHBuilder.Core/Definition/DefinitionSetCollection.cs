using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder
{
    public abstract class DefinitionSetCollection<T> : BuilderCollection<T> where T: IDefinitionSet
    {
        internal override void Save(string path)
        {
            
        }

        internal override void Load(string path)
        {
            
        }
    }
}
