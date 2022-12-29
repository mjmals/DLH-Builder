using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Terminal.Commands
{
    public interface ICommand
    {
        public string CommandPrefix { get; }

        public string Description { get; }

        public void Run(string[] commandHierarchy, out object returnObject);
    }
}
