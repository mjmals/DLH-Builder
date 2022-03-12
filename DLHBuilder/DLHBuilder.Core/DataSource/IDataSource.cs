using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataSource
    {
        Guid ID { get; set; }

        string Name { get; set; }
    }
}
