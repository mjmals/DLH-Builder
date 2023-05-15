using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;
using DLHApp.Model.Projects;
using DLHApp.Templates;

namespace DLHApp.Commands.Initialize
{
    public class InitializeCommand : Command, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "init" };

        public override string Description => "Creates a new project and folder structures in the current working directory, or in a new folder if a project name is supplied";

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
                Environment.CurrentDirectory = WorkingDirectory;
            }
            else
            {
                ProjectName = new DirectoryInfo(WorkingDirectory).Name;
            }

            Project.Initialize();
            TemplateImporter.Run();
        }
    }
}