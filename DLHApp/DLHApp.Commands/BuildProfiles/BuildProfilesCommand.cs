using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.BuildProfiles
{
    public class BuildProfilesCommand : Command, ITopLevelCommand, ICommand
    {
        public override string[] Prompt => new string[] { "buildprofile" };

        public override string Description => "Actions for creating and editing a BuildProfile";

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IBuildProfilesCommand));
        }
    }
}
