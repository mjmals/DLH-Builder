using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.Templates;

namespace DLHApp.Commands.Templates
{
    public class TemplatesAddCommand : Command, ITemplatesCommand, ICommand
    {
        public override string[] Prompt => new string[] { "add" };

        public override void Run(string[] args)
        {
            base.Run(args);

            if(Args.Length == 0)
            {
                WriteOutput("Please specify a Template name/path");
                return;
            }

            Args[Args.Length - 1] = Args.Last() + ".cshtml";
            string path = string.Join(@" ", Args);
            string templateFileName = Path.Combine(Environment.CurrentDirectory.Contains("Templates") ? "" : "Templates", Path.Combine(path));

            CreateFile(Path.GetFileName(templateFileName), Template.DefaultFileContent(), Path.GetDirectoryName(templateFileName));
        }
    }
}
