using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStages;

namespace DLHApp.Commands.DataStages
{
    public class DataStagesAddDatabricksCommand : Command, IDataStagesAddCommand, ICommand
    {
        public override string[] Prompt => new string[] { "dbx", "databricks" };

        public override void Run(string[] args)
        {
            base.Run(args);

            Args = string.Join(" ", Args).Split(@"\");

            string stageName = Args.LastOrDefault();
            string appPath = Args.Length <= 1 ? string.Empty : string.Join(" ", Args.Take(Args.Length - 1));

            if (string.IsNullOrEmpty(appPath))
            {
                WriteOutput("Please specify a valid Application Path");
                return;
            }

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Data Applications", appPath)))
            {
                WriteOutput(string.Format("Could not find application {0}", Path.Combine(Environment.CurrentDirectory, "Data Applications", appPath)));
                return;
            }


            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Data Applications", appPath, appPath + ".dbxapp.json")))
            {
                WriteOutput("Databricks Data Stage is not valid for this Application Type");
                return;
            }

            DatabricksDataStage stage = DatabricksDataStage.New(Path.Combine("Data Applications", appPath, "Stages"), stageName);
            stage.Save();
        }
    }
}
