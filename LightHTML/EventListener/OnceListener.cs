namespace LightHTML.EventListener
{
    class OnceListener : ILightEventListener
    {
        private readonly ILightEventListener _inner;
        private readonly string _eventType;

        public OnceListener(string eventType, ILightEventListener inner)
        {
            _eventType = eventType;
            _inner = inner;
        }

        public void HandleEvent(string eventType, LightElementNode target)
        {
            _inner.HandleEvent(eventType, target);
            target.RemoveEventListener(_eventType, this);
            Console.WriteLine($"OnceListener - автоматично відписано від \"{eventType}\"");
        }
    }
}