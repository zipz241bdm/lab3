using System.Text;
using LightHTML;

namespace Flyweight
{
    public class LightElementNodeFw : LightNode
    {
        private readonly ElementFlyweight _flyweight;

        public List<string> CssClasses { get; } = new();
        public List<LightNode> Children { get; } = new();

        public string TagName => _flyweight.TagName;
        public DisplayType Display => _flyweight.Display;
        public ClosingType Closing => _flyweight.Closing;
        public int ChildrenCount => Children.Count;

        public LightElementNodeFw(
            string tagName,
            DisplayType display = DisplayType.Block,
            ClosingType closing = ClosingType.WithClosingTag,
            IEnumerable<string>? cssClasses = null)
        {
            _flyweight = ElementFlyweightFactory.Get(tagName, display, closing);
            if (cssClasses is not null)
                CssClasses.AddRange(cssClasses);
        }

        public LightElementNodeFw AddChild(LightNode node) { Children.Add(node); return this; }

        public LightElementNodeFw AddClass(string cls) { CssClasses.Add(cls); return this; }

        private string OpenTag()
        {
            var sb = new StringBuilder($"<{TagName}");
            if (CssClasses.Count > 0)
                sb.Append($" class=\"{string.Join(' ', CssClasses)}\"");
            if (Closing == ClosingType.SelfClosing)
                sb.Append(" />");
            else
                sb.Append('>');
            return sb.ToString();
        }

        public override string InnerHTML(int indent = 0)
        {
            if (Closing == ClosingType.SelfClosing || Children.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var child in Children)
                sb.AppendLine(child.OuterHTML(indent));

            if (sb.Length > 0 && sb[^1] == '\n')
            {
                sb.Remove(sb.Length - 1, 1);
                if (sb.Length > 0 && sb[^1] == '\r') sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        public override string OuterHTML(int indent = 0)
        {
            var pad = Indent(indent);

            if (Closing == ClosingType.SelfClosing)
                return $"{pad}{OpenTag()}";

            var sb = new StringBuilder();
            sb.AppendLine($"{pad}{OpenTag()}");

            foreach (var child in Children)
                sb.AppendLine(child.OuterHTML(indent + 1));

            sb.Append($"{pad}</{TagName}>");
            return sb.ToString();
        }

        public override string ToString() => OuterHTML();
    }
}