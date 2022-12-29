using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Build;

namespace DLHBuilder.Azure.DataFactory.Build
{
    public class AzureDataFactoryPipelineBuildEngineOutput : IBuildEngineOutput
    {
        public string OutputFolder => "pipeline";

        public string Extension => ".json";

        public string[] TemplatePaths => new string[]
        {
            "Azure.Data_Factory.Pipeline."
        };
    }
}
