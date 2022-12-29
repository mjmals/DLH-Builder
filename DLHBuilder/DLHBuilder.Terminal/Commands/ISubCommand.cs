using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Terminal.Commands
{
    interface ISubCommand : ICommand
    {
        public Type ParentCommandType { get; }
    }
}
