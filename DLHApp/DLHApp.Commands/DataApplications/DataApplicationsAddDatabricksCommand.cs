using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataApplications;

namespace DLHApp.Commands.DataApplications
{
    public class DataApplicationsAddDatabricksCommand : Command, IDataApplicationsAddCommand, ICommand
    {
        public override string[] Prompt => new string[] { "dbx", "databricks" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput("Please specify a name for the SQL Server Application");
                return;
            }

            DatabricksDataApplication app = DatabricksDataApplication.New();
            app.Name = string.Join(" ", Args);
            app.Save();
        }
    }
}
