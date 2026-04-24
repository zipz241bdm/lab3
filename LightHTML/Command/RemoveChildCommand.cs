namespace LightHTML.Command
{
    public sealed class RemoveChildCommand : ILightCommand
    {
        private readonly LightElementNode _parent;
        private readonly LightNode _child;
        private int _savedIndex = -1;

        public string Name => $"RemoveChild {(_child.ToString() ?? "").ReplaceLineEndings(" ")} з <{_parent.TagName}>";

        public RemoveChildCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
        }

        public void Execute()
        {
            _savedIndex = _parent.Children.IndexOf(_child);
            if (_savedIndex < 0)
                throw new InvalidOperationException(
                    $"Вузол <{_child}> не є дочірнім елементом <{_parent.TagName}>.");

            _parent.RemoveChild(_child);
        }

        public void Undo()
        {
            if (_savedIndex >= 0 && _savedIndex <= _parent.Children.Count)
                _parent.Children.Insert(_savedIndex, _child);
            else
                _parent.AddChild(_child);
        }
    }
}
