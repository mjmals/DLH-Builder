using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Terminal.Commands
{
    class CommandHandler
    {
        public object CommandOutput { get; set; }

        public void RunCommand(string commandText)
        {
            object commandOutput = null;
             
            string[] commandHierarchy = commandText.ToLower().Split(" ");
            string commandPrefix = commandHierarchy[0];

            foreach(Type commandType in CommandList.Types())
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);

                if(command.CommandPrefix == commandPrefix)
                {
                    command.Run(commandHierarchy, out commandOutput);
                    CommandOutput = commandOutput;
                    return;
                }
            }

            CommandOutput = "command could not be found";
        }
    }
}
