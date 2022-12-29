using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Terminal.Commands
{
    class HelpCommand : ICommand
    {
        public string CommandPrefix => "help";

        public string Description => "Gets list of top level commands";

        public void Run(string[] commandHierarchy, out object returnObject)
        {
            returnObject = "";

            foreach(Type commandType in CommandList.Types())
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);
                returnObject += string.Format("\n{0}:\t\t{1}", command.CommandPrefix, command.Description);
            }
        }
    }
}
