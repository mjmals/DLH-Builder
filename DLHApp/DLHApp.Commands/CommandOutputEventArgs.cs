using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands
{
    public class CommandOutputEventArgs : EventArgs
    {
        public CommandOutputEventArgs(string output)
        {
            Output = output;
        }

        public string Output { get; set; }
    }
}
