using System.Text;
using LightHTML.ImageLoad;

namespace LightHTML
{
    public class LightImageNode : LightElementNode
    {
        const string tagName = "img";
        private ImageLoader _loader = new();
        public string Href 
        { 
            get; 
            set => field = _loader.AutoLoad(value); 
        }
        public string? AltText { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }

        public LightImageNode(string href,
                              string? alt = null,
                              IEnumerable<string>? cssClasses = null)
            : base(tagName, DisplayType.Inline, ClosingType.SelfClosing, cssClasses)
        {
            Href = href;
            AltText = alt;
        }

        public override string InnerHTML(int indent = 0) => string.Empty;

        public override string OuterHTML(int indent = 0)
        {
            var sb = new StringBuilder($"{Indent(indent)}<img");

            sb.Append($" src=\"{Href}\"");

            if (AltText is not null) sb.Append($" alt=\"{AltText}\"");
            if (Width is not null) sb.Append($" width=\"{Width}\"");
            if (Height is not null) sb.Append($" height=\"{Height}\"");

            if (CssClasses.Count > 0)
                sb.Append($" class=\"{string.Join(' ', CssClasses)}\"");

            sb.Append(" />");
            return sb.ToString();
        }

        public LightImageNode WithSize(string w, string h) { Width = w; Height = h; return this; }
    }
}
