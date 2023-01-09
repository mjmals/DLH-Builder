using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.BuildProfiles;

namespace DLHApp.Commands.BuildProfiles
{
    public class BuildProfilesAddCommand : Command, IBuildProfilesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput("Please supply Build Profile Name");
                return;
            }

            string profileName = Args[0];

            BuildProfile buildProfile = BuildProfile.New();
            buildProfile.Name = profileName;
            buildProfile.Save();
        }
    }
}
