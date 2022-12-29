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

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStructsCommand));
        }
    }
}
