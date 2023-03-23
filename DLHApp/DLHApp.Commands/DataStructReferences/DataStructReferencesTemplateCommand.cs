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

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructReferencesTemplateCommand));
        }
    }
}
