using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DLHBuilder
{
    public class DataStageCollection : BuilderCollection<IDataStage>
    {
        protected override string DirectoryName => "Data Stages";
    }
}
