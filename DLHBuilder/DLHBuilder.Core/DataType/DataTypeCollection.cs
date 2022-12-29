using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder
{
    public class DataTypeCollection : BuilderCollection<Type>
    {
        public DataTypeCollection()
        {
            foreach (Type type in typeof(IDataType).Assembly.GetTypes().OrderBy(x => x.FullName))
            {
                if (type.GetInterfaces().Contains(typeof(IDataType)))
                {
                    Add(type);
                }
            }
        }
    }
}
