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

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructReferencesCommand));
        }
    }
}
