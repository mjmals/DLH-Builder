using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public interface ICompiledDataArtifact
    {
        public string Name { get; }

        public IDataStage Stage { get; set; }

        public ICompiledSchemaItem[] Schema { get; set; }
    }
}
