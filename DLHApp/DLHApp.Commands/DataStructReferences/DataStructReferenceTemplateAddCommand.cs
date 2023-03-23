using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferenceTemplateAddCommand : Command, IDataStructReferencesTemplateCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override string Description => "Adds a specified template path to specified struct reference / struct reference path";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(!Args.Contains("refs"))
            {
                WriteOutput("Error: keyword 'refs' must be specified following template path");
                return;
            }

            int refsPointer = Args.ToList().IndexOf("refs");
            string templatePath = string.Join(" ", Args.Take(refsPointer)).Replace("\"", "");
            string refPath = string.Join(" ", Args.TakeLast(Args.Length - refsPointer - 1));

            if(!refPath.StartsWith("Data Applications"))
            {
                refPath = Path.Combine("Data Applications", refPath);
            }

            if (refPath.Split(@"\")[2].ToLower() != "stages")
            {
                List<string> refPathArray = refPath.Split(@"\").ToList();
                refPathArray.Insert(2, "Stages");
                refPath = string.Join(@"\", refPathArray);
            }

            foreach(string refFile in Directory.GetFiles(refPath, "*.ref.json", SearchOption.AllDirectories))
            {
                DataStructReference dsr = DataStructReference.Load(refFile);
                dsr.Templates.Add(templatePath);
                dsr.Save();
            }
        }
    }
}
