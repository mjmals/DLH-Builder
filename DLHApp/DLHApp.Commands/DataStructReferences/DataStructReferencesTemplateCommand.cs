using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferencesTemplateCommand : Command, IDataStructReferencesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "template", "tmp", "temp" };

        public override string Description => "Commands for controlling template collections in a struct reference";

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataStructReferencesTemplateCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructReferencesTemplateCommand));
        }
    }
}
