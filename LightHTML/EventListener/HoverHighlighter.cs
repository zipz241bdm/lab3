namespace LightHTML.EventListener
{
    class HoverHighlighter : ILightEventListener
    {
        public void HandleEvent(string eventType, LightElementNode target)
        {
            var action = eventType switch
            {
                "mouseover" => "підсвічено",
                "mouseout" => "підсвічення знято",
                _ => throw new ArgumentException($"Невідомий тип події: {eventType}")
            };
            Console.WriteLine($"HoverHighlighter - <{target.TagName}> {action}");
        }
    }
}