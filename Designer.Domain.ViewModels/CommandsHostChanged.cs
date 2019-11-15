namespace Designer.Domain.ViewModels
{
    public class CommandsHostChanged
    {
        public IDesignCommandsHost CommandsHost { get; }

        public CommandsHostChanged(IDesignCommandsHost commandsHost)
        {
            CommandsHost = commandsHost;
        }
    }
}