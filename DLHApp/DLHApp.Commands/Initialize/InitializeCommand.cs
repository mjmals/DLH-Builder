using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model;

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

            CreateFile("project.json", string.Empty, string.Empty);

            Type[] modelTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => x.IsAssignableTo(typeof(IModelItem)) && x.IsInterface == false && x.IsAbstract == false).ToArray();

            foreach(Type modelType in modelTypes)
            {
                IModelItem model = (IModelItem)Activator.CreateInstance(modelType);

                if(!Directory.Exists(model.BasePath) && !string.IsNullOrEmpty(model.BasePath))
                {
                    Directory.CreateDirectory(model.BasePath);
                }
            }
        }
    }
}