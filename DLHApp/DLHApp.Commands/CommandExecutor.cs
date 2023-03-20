using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DLHApp.Commands.Help;

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

            HelpCommand helpCmd = new HelpCommand();

            if(Args.Length > 1 && helpCmd.Prompt.Contains(Args.Last().ToLower()))
            {
                helpCmd.OutputWrite += OnCommandOutputWrite;
                helpCmd.RunCommandHelp(Args.Take(Args.Length - 1).ToArray());
                return;
            }

            searchType = searchType == null ? typeof(ITopLevelCommand) : searchType;

            Type[] commandTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(searchType) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach(Type commandType in commandTypes)
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);

                if (command.Prompt.Contains(Args[0].ToLower()))
                {
                    command.OutputWrite += OnCommandOutputWrite;
                    command.Run(Args);
                }
            }
        }

        void OnCommandOutputWrite(object sender, CommandOutputEventArgs e)
        {
            CommandOutputWrite?.Invoke(sender, e);
        }

        public EventHandler<CommandOutputEventArgs> CommandOutputWrite { get; set; }
    }
}
