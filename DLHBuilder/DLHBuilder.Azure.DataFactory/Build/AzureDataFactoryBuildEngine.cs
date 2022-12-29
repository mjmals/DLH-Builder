using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHBuilder.Build;
using DLHBuilder.Config;

namespace DLHBuilder.Azure.DataFactory.Build
{
    public class AzureDataFactoryBuildEngine : BuildEngine, IBuildEngine
    {
        public override string Name => "AzureDataFactory";

        public override string ConfigItem => "Build.AzureDataFactory";

        public override BuildEngineOutputCollection Outputs => new BuildEngineOutputCollection()
        {
            new AzureDataFactoryPipelineBuildEngineOutput()
        };
    }
}
