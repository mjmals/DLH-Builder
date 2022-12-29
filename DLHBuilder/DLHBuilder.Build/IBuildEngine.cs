using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Build
{
    public interface IBuildEngine
    {
        public string Name { get; set; }

        public string OutputPath { get; set; }

        public string ConfigItem { get; set; }

        public BuildEngineOutputCollection Outputs { get; set; }

        public void Run(Project project);
    }
}
