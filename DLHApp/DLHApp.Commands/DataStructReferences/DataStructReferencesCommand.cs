using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferencesCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "dsr", "refs" };

        public override string Description => "Commands for creating or editing references to a Data Struct in a Data Stage";

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataStructReferencesCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructReferencesCommand));
        }
    }
}
