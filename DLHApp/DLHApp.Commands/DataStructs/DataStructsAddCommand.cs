using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHApp.Commands.DataStructs
{
    public class DataStructsAddCommand : Command, ICommand, IDataStructsCommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput(@"Error: Please supply a name or {path}\name");
                return;
            }

            string[] structPath = Args[0].Split(@"\");
            string? structName = structPath.LastOrDefault();

            structPath = structPath.Length == 0 ? new string[0] : structPath.Take(structPath.Length - 1).ToArray();

            WriteOutput(string.Format("{0} (Path: {1})", structName, string.Join(@"\", structPath)));

            DataStruct ds =  DataStruct.New();
            ds.Name = structName;
            ds.FolderPath = Path.Combine(structPath);
            ds.Save();
        }
    }
}
