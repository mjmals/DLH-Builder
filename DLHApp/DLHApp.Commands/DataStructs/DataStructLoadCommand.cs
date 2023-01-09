using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DLHApp.Model.DataStructs;

namespace DLHApp.Commands.DataStructs
{
    public class DataStructLoadCommand : Command, IDataStructsCommand, ICommand
    {
        public override string[] Prompt => new string[] { "load" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if (Args[0].Length == 0)
            {
                WriteOutput("Please supply a path\name or name of a Data Structure");
                return;
            }

            DataStruct ds = DataStruct.Load(Args[0]);
        }
    }
}
