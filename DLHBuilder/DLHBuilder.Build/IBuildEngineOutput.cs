using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Build
{
    public interface IBuildEngineOutput
    {
        public string OutputFolder { get; }

        public string Extension { get; }

        public string[] TemplatePaths { get; }
    }
}
