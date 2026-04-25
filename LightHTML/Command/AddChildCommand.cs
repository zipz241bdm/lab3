namespace LightHTML.Command
{
    public sealed class AddChildCommand : ILightCommand
    {
        private readonly LightElementNode _parent;
        private readonly LightNode _child;

        public string Name => $"AddChild {(_child.ToString() ?? "").ReplaceLineEndings(" ")} до <{_parent.TagName}>";

        public AddChildCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
        }

        public void Execute() => _parent.AddChild(_child);

        public void Undo() => _parent.RemoveChild(_child);
    }
}
