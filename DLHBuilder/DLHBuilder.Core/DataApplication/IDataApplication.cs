using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface IDataApplication
    {
        string Name { get; set; }

        int Ordinal { get; set; }

        DataStageCollection Stages { get; set; }
    }
}
