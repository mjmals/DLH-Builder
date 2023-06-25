using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataPipelines
{
    public class DataPipelineTasksCommand : Command, ITopLevelCommand, ICommand
    {
        public override string[] Prompt => new string[] { "datapipelinetask", "dpltask" };

        public override string Description => "Actions for configuring tasks associated with a Data Pipeline";

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataPipelineTasksCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataPipelineTasksCommand));
        }
    }
}
