using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.Connections
{
    public class ConnectionsAddCommand : Command, IConnectionsCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override string Description => "Command for adding connections (include {type} parameter to specify type of connection)";

        public override Type[] SubCommandTypes => new Type[] { typeof(IConnectionsAddCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IConnectionsAddCommand));
        }
    }
}
