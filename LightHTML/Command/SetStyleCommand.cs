namespace LightHTML.Command
{
    public sealed class SetStyleCommand : ILightCommand
    {
        private readonly LightElementNode _element;
        private readonly string _property;
        private readonly string _newValue;
        private string? _previousValue;
        private bool _hadPrevious;

        public string Name => $"SetStyle \"{_property}:{_newValue}\" у <{_element.TagName}>";

        public SetStyleCommand(LightElementNode element, string property, string value)
        {
            _element = element;
            _property = property;
            _newValue = value;
        }

        public void Execute()
        {
            _hadPrevious = _element.Styles.TryGetValue(_property, out _previousValue);
            _element.AddStyle(_property, _newValue);
        }

        public void Undo()
        {
            if (_hadPrevious)
                _element.Styles[_property] = _previousValue!;
            else
                _element.Styles.Remove(_property);
        }
    }
}
