﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands.Connections
{
    public class ConnectionsAdd : Command, IConnectionsCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            RunSubCommand(typeof(IConnectionsAddCommand));
        }
    }
}