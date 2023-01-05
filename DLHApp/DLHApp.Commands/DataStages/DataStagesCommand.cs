using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.DataStages
{
    public class DataStagesCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "stages", "stg" };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IDataStagesCommand));
        }
    }
}
