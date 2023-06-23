using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataPipelines;

namespace DLHApp.Commands.DataPipelines
{
    public class DataPipelinesAddCommand : Command, IDataPipelinesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override string Description => "Creates new Data Pipeline";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput("Please supply a name or path\name combination for the Data Pipeline");
                return;
            }

            if(Args.Length > 1)
            {
                Args = new string[] { string.Join(" ", Args) };
            }

            string pipelineName = Args[0].Split(@"\").LastOrDefault();
            string pipelineFolder = string.Empty;

            if (Args[0].Split(@"\").Length > 1)
            {
                pipelineFolder = string.Join(@"\", Args[0].Split(@"\").Take(Args[0].Split(@"\").Length - 1));
            }

            DataPipeline pipeline = DataPipeline.New();
            pipeline.Name = pipelineName;
            pipeline.FolderPath = pipelineFolder;
            pipeline.Save();
        }
    }
}
