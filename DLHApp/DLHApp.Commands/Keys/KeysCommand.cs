using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.Keys
{
    public class KeysCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "keys" };

        public override string Description => "Commands for creating and editing Keys";

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IKeysCommand));
        }
    }
}
