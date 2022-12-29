using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.BuildProfiles
{
    public class BuildProfileStage
    {
        public string Name { get; set; }

        public string OutputPath { get; set; }

        public string[] Templates { get; set; }
    }
}
