using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataPipelines
{
    public class DataPipelinesCommand : Command, ITopLevelCommand, ICommand
    {
        public override string[] Prompt => new string[] { "datapipeline", "dpl" };

        public override string Description => "Actions for creating and editing Data Pipelines";

        public override Type[] SubCommandTypes => new Type[] { typeof(IDataPipelinesCommand) };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataPipelinesCommand));
        }
    }
}
