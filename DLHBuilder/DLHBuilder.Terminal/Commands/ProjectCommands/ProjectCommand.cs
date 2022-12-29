using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Terminal.Commands.ProjectCommands
{
    class ProjectCommand : CommandExecutor, ICommand
    {
        public string CommandPrefix => "project";

        public string Description => "Allows creating, opening and saving of projects";

        public object ReturnObject { get; set; }
    }
}
