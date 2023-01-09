using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.Keys;

namespace DLHApp.Commands.Keys
{
    public class KeysAddCommand : Command, IKeysCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput("Please specify a Key name/path");
                return;
            }

            string[] keyPath = Args[0].Split(@"\");
            string? keyName = keyPath.LastOrDefault();
            keyPath = keyPath.Length == 0 ? new string[0] : keyPath.Take(keyPath.Length - 1).ToArray();

            Key key = Key.New();
            key.Name = keyName;
            key.FolderPath = Path.Combine(keyPath);
            key.Save();
        }
    }
}
