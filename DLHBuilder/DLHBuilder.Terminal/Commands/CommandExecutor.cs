using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DLHBuilder.Terminal.Commands
{
    abstract class CommandExecutor
    {
        public virtual void Run(string[] commandHierachy, out object returnObject)
        {
            returnObject = null;

            if(commandHierachy.Length == 1)
            {
                // todo: handle list of sub commands
                return;
            }

            Type[] subCommandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo(typeof(ISubCommand))
                    && x.IsInterface == false)
                .ToArray();

            foreach(Type subCommandType in subCommandTypes)
            {
                ISubCommand command = (ISubCommand)Activator.CreateInstance(subCommandType);
                
                if(command.ParentCommandType == this.GetType() && command.CommandPrefix == commandHierachy[1])
                {
                    command.Run(commandHierachy, out returnObject);
                    return;
                }
            }
        }
    }
}
