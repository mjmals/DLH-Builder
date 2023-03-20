namespace DLHApp.Commands
{
    public interface ICommand
    {
        public string[] Prompt { get; }

        public string WorkingDirectory { get; }

        public string Description { get; }

        public Type[] SubCommandTypes { get; }

        void Run(string[] args);

        void RunSubCommand(Type commandType);

        public EventHandler<CommandOutputEventArgs> OutputWrite { get; set; }
    }
}