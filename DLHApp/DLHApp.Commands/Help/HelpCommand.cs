using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHApp.Commands.Help
{
    internal class HelpCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "help", "?" };

        public override string Description => "Returns help for application commands";

        public override void Run(string[] args)
        {
            base.Run(args);

            Type[] commandTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(typeof(ITopLevelCommand)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            Dictionary<string, string> commands = new Dictionary<string, string>();
            int maxPromptLength = 0;

            foreach (Type commandType in commandTypes)
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);

                string promptValues = string.Join(",", command.Prompt);
                maxPromptLength = promptValues.Length > maxPromptLength ? promptValues.Length : maxPromptLength;

                commands.Add(promptValues, command.Description);
            }

            Dictionary<string, string> commandsOrdered = new Dictionary<string, string>();

            foreach(KeyValuePair<string,string> command in commands.OrderBy(x => x.Key))
            {
                commandsOrdered.Add(command.Key, command.Value);
            }

            WriteHelpOutput(commandsOrdered);
        }

        public void RunCommandHelp(string[] args)
        {
            Dictionary<string, string> commands = new Dictionary<string, string>();
            ICommand command = null;
            Type[] commandTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(typeof(ICommand)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            for (int i = 0; i < args.Length; i++)
            {
                if(command == null)
                {
                    foreach(Type cmdType in commandTypes)
                    {
                        ICommand cmd = (ICommand)Activator.CreateInstance(cmdType);

                        if (cmd.Prompt.Contains(args[i].ToLower()))
                        {
                            command = cmd;
                        }
                    }
                }

                foreach(Type cmdType in command.SubCommandTypes)
                {
                    Type[] subCmdTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(cmdType) && x.IsInterface == false && x.IsAbstract == false).ToArray();

                    foreach (Type subType in subCmdTypes)
                    {
                        ICommand cmd = (ICommand)Activator.CreateInstance(subType);

                        if (cmd.Prompt.Contains(args[i].ToLower()))
                        {
                            command = cmd;
                            continue;
                        }
                    }
                }
            }

            commands.Add(string.Join(",", command.Prompt), command.Description);
            GetSubCommandHelp(command, 1, commands);
            WriteHelpOutput(commands);
        }

        void GetSubCommandHelp(ICommand command, int level, Dictionary<string,string> commandList)
        {
            foreach(Type cmdType in command.SubCommandTypes)
            {
                Type[] subCmdTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsAssignableTo(cmdType) && x.IsInterface == false && x.IsAbstract == false).ToArray();
                
                foreach(Type subType in subCmdTypes.OrderBy(x => x.Name))
                {
                    ICommand cmd = (ICommand)Activator.CreateInstance(subType);
                    commandList.Add(new string(' ', level * 2) + string.Join(",", cmd.Prompt), cmd.Description);
                    GetSubCommandHelp(cmd, level + 1, commandList);
                }
            }
        }

        void WriteHelpOutput(Dictionary<string,string> commands)
        {
            int maxPromptLength = commands.Max(x => x.Key.Length);

            WriteOutput("\nPrompt(s)".PadRight(maxPromptLength) + "\t \t" + "Description\n");

            foreach (KeyValuePair<string, string> helpValue in commands)
            {
                string promptValue = helpValue.Key.PadRight(maxPromptLength);
                string descValue = helpValue.Value;

                WriteOutput(string.Format("{0}\t:\t{1}", promptValue, descValue));
            }

            WriteOutput("\n");
        }
    }
}
