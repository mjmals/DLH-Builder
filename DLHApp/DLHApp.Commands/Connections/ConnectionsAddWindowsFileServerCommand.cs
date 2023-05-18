using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.Connections;

namespace DLHApp.Commands.Connections
{
    internal class ConnectionsAddWindowsFileServerCommand : Command, ICommand, IConnectionsAddCommand
    {
        public override string[] Prompt => new string[] { "wfs" };

        public override string Description => "Creates a new Windows File Server connection file";

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Count() == 0)
            {
                WriteOutput("Please specify file server and folder path");
                return;
            }

            string fileServerPath = string.Join(" ", Args);

            try
            {
                WindowsFileServerConnection conn = WindowsFileServerConnection.New();
                conn.FullServerPath = fileServerPath;
                conn.Save();

                WriteOutput(string.Format("Successfully created connection \"{0}\"\n", conn.Name));
            }
            catch(Exception e)
            {
                WriteOutput(string.Format("Failed to create connection:\n{0}\n", e.Message));
            }
        }
    }
}
