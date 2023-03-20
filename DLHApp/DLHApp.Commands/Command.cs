using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Commands
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            Args = new string[0];
        }

        public virtual string[] Prompt => new string[0];

        public virtual string WorkingDirectory => Environment.CurrentDirectory;

        public virtual string Description => "No Description";

        public virtual Type[] SubCommandTypes => new Type[0];

        protected virtual string[] Args { get; set; }

        public virtual string GetInput(string requestText)
        {
            Console.Write(requestText + ": ");
            string? inputText = Console.ReadLine();
            return !string.IsNullOrEmpty(inputText) ? inputText : string.Empty;
        }

        public virtual void Run(string[] args)
        {
            if(args.Length > 1)
            {
                Args = args.Skip(1).ToArray();
                args = Args;
            }
        }

        public virtual void RunSubCommand(Type commandType)
        {
            CommandExecutor cmdExec = new CommandExecutor(Args);
            CommandExecutor existingExec = (CommandExecutor)OutputWrite.Target;
            cmdExec.CommandOutputWrite += existingExec.CommandOutputWrite;
            cmdExec.Run(commandType);
        }

        protected virtual void CreateFolder(string name)
        {
            Directory.CreateDirectory(Path.Combine(WorkingDirectory, name));
        }

        protected virtual void CreateFile(string name, string content, string path)
        {
            string filePath = Path.Combine(WorkingDirectory, path);

            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (FileStream fs = new FileStream(Path.Combine(filePath, name), FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                }
            }
        }

        public string[] GetEnumValues(Type enumType)
        {
            return Enum.GetNames(enumType);
        }

        public string GetEnumValuesString(Type enumType)
        {
            string[] values = GetEnumValues(enumType);
            return string.Join(" | ", values);
        }

        public bool TestEnumValue(Type enumType, string value)
        {
            try
            {
                ParseEnumValue(enumType, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public object ParseEnumValue(Type enumType, string value)
        {
            return Enum.Parse(enumType, value);
        }

        protected void WriteOutput(string output)
        {
            OutputWrite?.Invoke(this, new CommandOutputEventArgs(output));
        }

        public EventHandler<CommandOutputEventArgs> OutputWrite { get; set; }
    }
}
