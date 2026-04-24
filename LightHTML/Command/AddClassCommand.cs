namespace LightHTML.Command
{
    public sealed class AddClassCommand : ILightCommand
    {
        private readonly LightElementNode _element;
        private readonly string _cssClass;

        public string Name => $"AddClass \"{_cssClass}\" в <{_element.TagName}>";

        public AddClassCommand(LightElementNode element, string cssClass)
        {
            _element = element;
            _cssClass = cssClass;
        }

        public void Execute() => _element.AddClass(_cssClass);

        public void Undo() => _element.CssClasses.Remove(_cssClass);
    }
}
