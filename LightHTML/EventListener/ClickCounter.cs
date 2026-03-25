namespace LightHTML.EventListener
{
    class ClickCounter : ILightEventListener
    {
        public int Count { get; private set; }

        public void HandleEvent(string eventType, LightElementNode target)
        {
            if (eventType.Equals("click", StringComparison.OrdinalIgnoreCase))
            {
                Count++;
                Console.WriteLine($"ClickCounter - <{target.TagName}> натиснуто {Count} разів");
            }
        }
    }
}