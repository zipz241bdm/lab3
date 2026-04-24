namespace LightHTML
{
    public class LightTextNode : LightNode
    {
        public string Text { get; set; }

        public LightTextNode(string text)
        {
            Text = text;
            OnCreated();
        }

        public override string OuterHTML(int indent = 0)
        {
            var result = $"{Indent(indent)}{Text}";
            OnTextRendered(Text);
            return result;
        }

        public override string InnerHTML(int indent = 0) => OuterHTML(indent);

        public override string ToString() => Text;
    }
}
