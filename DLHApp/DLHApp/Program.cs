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
            //Console.Write("Please enter working directory: ");
            //Environment.CurrentDirectory = Console.ReadLine();

            //args = new string[] { "keys", "add" };

            //Console.Write("$ ");
            //args = Console.ReadLine().Split(" ");

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