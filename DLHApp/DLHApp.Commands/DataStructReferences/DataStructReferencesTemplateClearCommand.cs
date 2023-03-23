using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferencesTemplateClearCommand : Command, IDataStructReferencesTemplateCommand, ICommand
    {
        public override string[] Prompt => new string[] { "clear" };

        public override string Description => "Clears all template paths from struct refs under the specified struct ref path.";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput("Struct ref path must be supplied");
                return;
            }

            string refPath = string.Join(" ", Args);

            if (!refPath.StartsWith("Data Applications"))
            {
                refPath = Path.Combine("Data Applications", refPath);
            }

            if (refPath.Split(@"\")[2].ToLower() != "stages")
            {
                List<string> refPathArray = refPath.Split(@"\").ToList();
                refPathArray.Insert(2, "Stages");
                refPath = string.Join(@"\", refPathArray);
            }

            foreach(string refFile in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, refPath), "*.ref.json", SearchOption.AllDirectories))
            {
                DataStructReference dsr = DataStructReference.Load(refFile);
                dsr.Templates.Clear();
                dsr.Save();
            }
        }
    }
}
