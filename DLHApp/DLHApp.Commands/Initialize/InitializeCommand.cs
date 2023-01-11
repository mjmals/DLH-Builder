using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;

namespace DLHApp.Commands.Initialize
{
    public class InitializeCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "init" };

        public string ProjectName { get; set; }

        bool IncludeProjectName = false;

        public override string WorkingDirectory => Path.Combine(base.WorkingDirectory, IncludeProjectName ? ProjectName : string.Empty);

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length > 0)
            {
                ProjectName = Args[0];
                IncludeProjectName = true;
                Directory.CreateDirectory(WorkingDirectory);
            }
            else
            {
                ProjectName = new DirectoryInfo(WorkingDirectory).Name;
            }

            Project.Initialize();
        }
    }
}