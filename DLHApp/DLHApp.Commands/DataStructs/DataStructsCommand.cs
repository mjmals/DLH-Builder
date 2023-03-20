using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStructs
{
    public class DataStructsCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "datastruct", "ds" };

        public override string Description => "Commands for creating or importing Data Structures from a source connection";

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataStructsCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructsCommand));
        }
    }
}
