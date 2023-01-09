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
            //Console.Write("Please enter work0ing directory: ");
            //Environment.CurrentDirectory = Console.ReadLine();

            //args = new string[] { "keys", "add" };

            CommandExecutor cmdExec = new CommandExecutor(args);
            cmdExec.CommandOutputWrite += WriteOutput;
            cmdExec.Run();
        }

        static void WriteOutput(object sender, CommandOutputEventArgs e)
        {
            Console.WriteLine(e.Output);
        }
    }
}