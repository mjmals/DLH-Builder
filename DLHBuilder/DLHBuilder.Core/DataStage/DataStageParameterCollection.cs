using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataStageParameterCollection : List<DataStageParameter>
    {
        public void Add(string name)
        {
            DataStageParameter param = new DataStageParameter(name);

            if(!Exists(x => x.Name == param.Name))
            {
                Add(param);
            }
        }
    }
}
