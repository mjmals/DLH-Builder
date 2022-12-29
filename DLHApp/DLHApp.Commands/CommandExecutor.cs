using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Commands
{
    public class CommandExecutor
    {
        public CommandExecutor(string[] args)
        {
            Args = args;
        }

        string[] Args { get; set; }

        public void Run(Type? searchType = null)
        {
            if(Args.Length == 0)
            {
                return;
            }

            searchType = searchType == null ? typeof(ITopLevelCommand) : searchType;

            Type[] commandTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(searchType) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach(Type commandType in commandTypes)
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);

                if (command.Prompt.Contains(Args[0].ToLower()))
                {
                    command.Run(Args);
                }
            }
        }
    }
}
