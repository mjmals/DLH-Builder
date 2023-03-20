using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Build;

namespace DLHApp.Commands.Builds
{
    internal class BuildCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "build" };

        public override string Description => "Performs build against a specified buildprofile to compile templates to a specified output location";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Count() == 0)
            {
                WriteOutput("Please supply a Build Profile name");
                return;
            }

            BuildEngine build = new BuildEngine(Args[0]);
            build.Run();
        }
    }
}
