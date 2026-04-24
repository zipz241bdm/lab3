using System.Text;

namespace LightHTML.Visitor
{
    public class PlainTextExtractorVisitor : ILightNodeVisitor
    {
        private readonly StringBuilder _textBuilder = new();

        public string GetPlainText() => _textBuilder.ToString().Trim();

        public void Visit(LightElementNode node)
        {
            if (node.Display == DisplayType.Block && _textBuilder.Length > 0)
            {
                _textBuilder.AppendLine();
            }
        }

        public void Visit(LightTextNode node)
        {
            _textBuilder.Append(node.Text).Append(' ');
        }

        public void Visit(LightImageNode node)
        {
            if (!string.IsNullOrEmpty(node.AltText))
            {
                _textBuilder.Append($"[{node.AltText}] ");
            }
        }
    }
}