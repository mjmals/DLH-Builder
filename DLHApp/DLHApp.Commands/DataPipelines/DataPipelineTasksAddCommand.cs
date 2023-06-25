using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;

namespace DLHApp.Commands.DataPipelines
{
    public class DataPipelineTasksAddCommand : Command, IDataPipelineTasksCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override string Description => "Creates a new Data Pipeline Task";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length <= 1)
            {
                WriteOutput(@"Please specify a pipeline and task name with a format of <pipeline path>\<task name>");
                return;
            }

            if(Args.Length > 1)
            {
                Args = string.Join(@" ", Args).Split(@"\");
            }

            string taskName = Args.Last();
            string pipelinePath = Path.Combine("Data Pipelines", string.Join(@"\", Args.Take(Args.Length - 1)));
            string pipelineName = pipelinePath.Split(@"\").Last();
            string pipelineFile = Path.Combine(pipelinePath, pipelineName + ".dpl.json");

            if(!File.Exists(pipelineFile))
            {
                WriteOutput(string.Format("Could not find a pipeline named {0}", pipelinePath));
                return;
            }

            DataPipelineTask task = DataPipelineTask.New();
            task.Name = taskName;
            task.FolderPath = Path.Combine(pipelinePath, "Tasks");
            task.Save();
        }
    }
}
