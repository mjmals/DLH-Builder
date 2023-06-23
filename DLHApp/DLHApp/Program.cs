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
            bool execute = true;

            //args = new string[] { "start" };

            if(args.Length > 0 && args[0] == "start")
            {
                while(execute)
                {
                    Console.Write("> ");
                    args = Console.ReadLine().Split(@" ");

                    if (args[0].ToLower() == "exit")
                    {
                        execute = false;
                        continue;
                    }

                    RunCommand(args);
                }

                return;
            }

            RunCommand(args);
        }

        static void WriteOutput(object sender, CommandOutputEventArgs e)
        {
            Console.WriteLine(e.Output);
        }

        static void RunCommand(string[] args)
        {
            CommandExecutor cmdExec = new CommandExecutor(args);
            cmdExec.CommandOutputWrite += WriteOutput;
            cmdExec.Run();
        }
    }
}