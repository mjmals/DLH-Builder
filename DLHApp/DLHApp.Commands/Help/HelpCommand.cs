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

            Dictionary<string, string> commandHelper = new Dictionary<string, string>();
            int maxPromptLength = 0;

            foreach (Type commandType in commandTypes)
            {
                ICommand command = (ICommand)Activator.CreateInstance(commandType);

                string promptValues = string.Join(",", command.Prompt);
                maxPromptLength = promptValues.Length > maxPromptLength ? promptValues.Length : maxPromptLength;

                commandHelper.Add(promptValues, string.IsNullOrEmpty(command.Description) ? "No Description" : command.Description);
            }

            WriteOutput("\nPrompt(s)".PadRight(maxPromptLength) + "\t \t" + "Description\n");

            foreach(KeyValuePair<string, string> helpValue in commandHelper.OrderBy(x => x.Key))
            {
                string promptValue = helpValue.Key.PadRight(maxPromptLength);
                string descValue = helpValue.Value;

                WriteOutput(string.Format("{0}\t:\t{1}", promptValue, descValue));
            }

            WriteOutput("\n");
        }
    }
}
