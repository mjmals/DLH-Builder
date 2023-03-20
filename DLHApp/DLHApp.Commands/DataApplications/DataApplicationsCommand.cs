using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataApplications
{
    public class DataApplicationsCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "applications", "apps", "da" };

        public override string Description => "Enables creation and editing of Data Applications";

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataApplicationsCommand));
        }
    }
}
