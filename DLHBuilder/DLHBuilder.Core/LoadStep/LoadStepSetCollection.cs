using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class LoadStepSetCollection : BuilderCollection<LoadStepSet>
    {
        public new void Add(LoadStepSet set)
        {
            base.Add(set);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Ordinal = i + 1;
            }
        }
    }
}
