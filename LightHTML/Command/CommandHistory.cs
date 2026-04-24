namespace LightHTML.Command
{
    public sealed class CommandHistory
    {
        private readonly Stack<ILightCommand> _history = new();
        private readonly Stack<ILightCommand> _redoStack = new();

        public bool CanUndo => _history.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void Execute(ILightCommand command)
        {
            command.Execute();
            _history.Push(command);
            _redoStack.Clear();

            Console.WriteLine($"History - Виконано: {command.Name}");
        }

        public bool Undo()
        {
            if (!CanUndo)
            {
                Console.WriteLine("History - Немає команд для скасування.");
                return false;
            }

            var command = _history.Pop();
            command.Undo();
            _redoStack.Push(command);

            Console.WriteLine($"History - Скасовано: {command.Name}");
            return true;
        }

        public bool Redo()
        {
            if (!CanRedo)
            {
                Console.WriteLine("History - Немає команд для повтору.");
                return false;
            }

            var command = _redoStack.Pop();
            command.Execute();
            _history.Push(command);

            Console.WriteLine($"History - Повторено: {command.Name}");
            return true;
        }

        public void PrintState()
        {
            Console.WriteLine($"History - Стан: history={_history.Count}, redo={_redoStack.Count}");

            if (_history.Count > 0)
            {
                Console.WriteLine("  Undo-стек (від останньої):");
                foreach (var cmd in _history)
                    Console.WriteLine($"    - {cmd.Name}");
            }

            if (_redoStack.Count > 0)
            {
                Console.WriteLine("  Redo-стек (від останньої):");
                foreach (var cmd in _redoStack)
                    Console.WriteLine($"    - {cmd.Name}");
            }
        }
    }
}
