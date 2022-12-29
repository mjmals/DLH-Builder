namespace DLHApp.Commands
{
    public interface ICommand
    {
        public string[] Prompt { get; }

        public string WorkingDirectory { get; }

        void Run(string[] args);

        void RunSubCommand(Type commandType);
    }
}