using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataSourceCollection : BuilderCollection<IDataSource>
    {
        protected override string DirectoryName => "Data Sources";
    }
}
