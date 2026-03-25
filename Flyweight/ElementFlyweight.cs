using LightHTML;

namespace Flyweight
{
    public class ElementFlyweight
    {
        public string TagName { get; }
        public DisplayType Display { get; }
        public ClosingType Closing { get; }

        internal ElementFlyweight(string tagName, DisplayType display, ClosingType closing)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
        }

        public override string ToString() => $"Flyweight(<{TagName}>, {Display}, {Closing})";
    }
}