namespace LightHTML.EventListener
{
    class ConsoleLogger : ILightEventListener
    {
        private readonly string _name;
        public ConsoleLogger(string name = "Logger") => _name = name;

        public void HandleEvent(string eventType, LightElementNode target)
        {
            Console.WriteLine($"{_name} - подія \"{eventType}\" на <{target.TagName}>" +
                              (target.CssClasses.Count > 0
                                  ? $" .{string.Join('.', target.CssClasses)}"
                                  : string.Empty));
        }
    }
}