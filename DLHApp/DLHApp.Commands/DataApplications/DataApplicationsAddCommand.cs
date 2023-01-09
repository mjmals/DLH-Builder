using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataApplications
{
    public class DataApplicationsAddCommand : Command, IDataApplicationsCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput("Please specify a type of Data Application to create");
                return;
            }

            RunSubCommand(typeof(IDataApplicationsAddCommand));
        }
    }
}
