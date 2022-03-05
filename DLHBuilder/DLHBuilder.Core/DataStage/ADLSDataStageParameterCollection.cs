using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class ADLSDataStageParameterCollection : BuilderCollection<ADLSDataStageParameter>
    {
        public void Add(string name)
        {
            ADLSDataStageParameter param = new ADLSDataStageParameter(name);

            if(!Exists(x => x.Name == param.Name))
            {
                Add(param);
            }
        }
    }
}
