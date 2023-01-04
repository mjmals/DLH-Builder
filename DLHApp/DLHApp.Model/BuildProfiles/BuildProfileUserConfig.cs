using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.BuildProfiles
{
    public class BuildProfileUserConfig
    {
        public string TargetFolder { get; set; }

        public override string ToString()
        {
            return "Build Profile User Config";
        }
    }
}
