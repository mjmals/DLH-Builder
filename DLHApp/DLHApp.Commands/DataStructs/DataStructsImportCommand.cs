using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStructs
{
    public class DataStructsImportCommand : Command, IDataStructsCommand, ICommand
    {
        public override string[] Prompt => new string[] { "import" };

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataStructsImportCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput(@"Please specify a connection type");
                return;
            }

            RunSubCommand(typeof(IDataStructsImportCommand));
        }
    }
}
