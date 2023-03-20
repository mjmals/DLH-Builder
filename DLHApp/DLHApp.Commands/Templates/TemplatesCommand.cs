using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.Templates
{
    public class TemplatesCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "templates" };

        public override string Description => "Commands for creating new templates";

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(ITemplatesCommand));
        }
    }
}
