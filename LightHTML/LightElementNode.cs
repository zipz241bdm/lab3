using System.Text;
using LightHTML.EventListener;

namespace LightHTML
{
    public class LightElementNode : LightNode
    {
        public string TagName { get; set; }
        public DisplayType Display { get; set; }
        public ClosingType Closing { get; set; }
        public List<string> CssClasses { get; private set; } = new();
        public Dictionary<string, string> Styles { get; private set; } = new(StringComparer.OrdinalIgnoreCase);
        public List<LightNode> Children { get; private set; } = new();
        public int ChildrenCount => Children.Count;

        private readonly Dictionary<string, List<ILightEventListener>> _listeners = new(StringComparer.OrdinalIgnoreCase);

        public LightElementNode(
            string tagName,
            DisplayType display = DisplayType.Block,
            ClosingType closing = ClosingType.WithClosingTag,
            IEnumerable<string>? cssClasses = null)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
            if (cssClasses is not null)
                CssClasses.AddRange(cssClasses);

            OnCreated();
        }

        public virtual LightElementNode AddChild(LightNode node)
        {
            Children.Add(node);
            OnInserted(node);
            return this;
        }

        public virtual LightElementNode RemoveChild(LightNode node)
        {
            if (Children.Remove(node))
                OnRemoved(node);
            return this;
        }

        public virtual LightElementNode AddClass(string cls)
        {
            CssClasses.Add(cls);
            OnClassListApplied(cls);
            return this;
        }

        public virtual LightElementNode AddStyle(string property, string value)
        {
            Styles[property] = value;
            OnStylesApplied(property, value);
            return this;
        }

        public virtual LightElementNode AddEventListener(string eventType, ILightEventListener listener)
        {
            if (!_listeners.TryGetValue(eventType, out var list))
            {
                list = new List<ILightEventListener>();
                _listeners[eventType] = list;
            }

            if (!list.Contains(listener))
                list.Add(listener);

            return this;
        }

        public virtual LightElementNode RemoveEventListener(string eventType, ILightEventListener listener)
        {
            if (_listeners.TryGetValue(eventType, out var list))
                list.Remove(listener);
            return this;
        }

        public virtual void DispatchEvent(string eventType)
        {
            if (!_listeners.TryGetValue(eventType, out var list) || list.Count == 0)
            {
                Console.WriteLine($"<{TagName}> Подія \"{eventType}\" - слухачів немає.");
                return;
            }

            foreach (var listener in list.ToList())
                listener.HandleEvent(eventType, this);
        }

        private string OpenTag()
        {
            var sb = new StringBuilder($"<{TagName}");
            if (CssClasses.Count > 0)
                sb.Append($" class=\"{string.Join(' ', CssClasses)}\"");
            if (Styles.Count > 0)
                sb.Append($" style=\"{string.Join("; ", Styles.Select(kv => $"{kv.Key}:{kv.Value}"))}\"");
            sb.Append(Closing == ClosingType.SelfClosing ? " />" : ">");
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

        public override void Accept(Visitor.ILightNodeVisitor visitor)
        {
            visitor.Visit(this);
            
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
        }
    }
}