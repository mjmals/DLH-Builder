﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.Connections
{
    public class Connection : Command, ICommand, ITopLevelCommand
    {
        public override string[] Prompt => new string[] { "connections", "conn" };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IConnectionsCommand));
        }
    }
}
