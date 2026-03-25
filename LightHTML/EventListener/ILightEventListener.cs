namespace LightHTML.EventListener
{
    public interface ILightEventListener
    {
        void HandleEvent(string eventType, LightElementNode target);
    }
}