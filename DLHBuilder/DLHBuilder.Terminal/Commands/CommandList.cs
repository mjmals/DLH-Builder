using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Terminal.Commands
{
    class CommandList
    {
        public static List<Type> Types()
        {
            return 
                AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(typeof(ICommand))
                    && x.IsAssignableTo(typeof(ISubCommand)) == false
                    && x.IsInterface == false)
                .ToList();
        }
    }
}
