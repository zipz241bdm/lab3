namespace LightHTML.Command
{
    public sealed class MacroCommand : ILightCommand
    {
        private readonly List<ILightCommand> _commands;

        public string Name { get; }

        public MacroCommand(string name, IEnumerable<ILightCommand> commands)
        {
            Name = name;
            _commands = new(commands);
        }

        public void Execute()
        {
            foreach (var cmd in _commands)
                cmd.Execute();
        }

        public void Undo()
        {
            for (int i = _commands.Count - 1; i >= 0; i--)
                _commands[i].Undo();
        }
    }
}
