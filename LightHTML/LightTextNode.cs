namespace LightHTML
{
    public class LightTextNode : LightNode
    {
        public string Text { get; set; }

        public LightTextNode(string text)
        {
            Text = text;
        }

        public override string OuterHTML(int indent = 0) => $"{Indent(indent)}{Text}";
        public override string InnerHTML(int indent = 0) => $"{Indent(indent)}{Text}";

        public override string ToString() => Text;
    }
}