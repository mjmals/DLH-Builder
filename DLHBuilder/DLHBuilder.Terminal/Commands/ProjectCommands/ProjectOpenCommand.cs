using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Terminal.Commands.ProjectCommands
{
    class ProjectOpenCommand : ISubCommand
    {
        public Type ParentCommandType => typeof(ProjectCommand);

        public string CommandPrefix => "open";

        public string Description => "Opens a project from a specified or selected file";

        public void Run(string[] commandHierarchy, out object returnObject)
        {
            returnObject = null;

            if (commandHierarchy.Length > 2)
            {
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog() { Filter = "DLH Builder Project Files | *.json" };
            dialog.ShowDialog();

            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                returnObject = Project.Load(dialog.FileName);
                return;
            }

            returnObject = "Error: a project file must be specified or selected by the open file dialog";
        }
    }
}
