using Newtonsoft.Json;
using System.Text.RegularExpressions;
using DLHApp.Model;
using DLHApp.Model.DataStructs;
using DLHApp.Commands;

namespace DLHApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "build", "DataFactory" };

            CommandExecutor cmdExec = new CommandExecutor(args);
            cmdExec.Run();
        }
    }
}