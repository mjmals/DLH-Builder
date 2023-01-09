using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStages
{
    public class DataStagesAddCommand : Command, IDataStagesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput("Please specify a Data Stage type");
                return;
            }

            RunSubCommand(typeof(IDataStagesAddCommand));
        }
    }
}
