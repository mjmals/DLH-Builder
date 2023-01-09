using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructReferences;

namespace DLHApp.Commands.DataStructReferences
{
    public class DataStructReferencesAddCommand : Command, IDataStructReferencesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args.Length == 0)
            {
                WriteOutput("Please specify a Reference name/path");
                return;
            }

            string refName = Args.LastOrDefault();
            Args = Args.Take(Args.Length - 1).ToArray();
            string refPath = string.Join(" ", Args);

            DataStructReference dsr = DataStructReference.New();
            dsr.Name = refName;
            dsr.FolderPath = refPath;
            dsr.Save();
        }
    }
}
