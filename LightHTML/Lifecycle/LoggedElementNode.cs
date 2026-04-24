namespace LightHTML.Lifecycle
{
    public class LoggedElementNode(
        string tagName,
        string label = "",
        DisplayType display = DisplayType.Block,
        ClosingType closing = ClosingType.WithClosingTag,
        IEnumerable<string>? cssClasses = null) 
        : LightElementNode(tagName, display, closing, cssClasses)
    {
        private readonly string _label = string.IsNullOrEmpty(label) ? tagName : label;

        protected override void OnCreated()
            => Log("OnCreated", $"<{TagName}> створено");

        protected override void OnInserted(LightNode child)
        {
            var desc = child is LightElementNode el ? $"<{el.TagName}>"
                     : child is LightTextNode tx ? $"\"{Truncate(tx.Text)}\""
                     : child.GetType().Name;
            Log("OnInserted", $"дитина {desc} додана до <{TagName}>");
        }

        protected override void OnRemoved(LightNode child)
        {
            var desc = child is LightElementNode el ? $"<{el.TagName}>"
                     : child is LightTextNode tx ? $"\"{Truncate(tx.Text)}\""
                     : child.GetType().Name;
            Log("OnRemoved", $"дитина {desc} видалена з <{TagName}>");
        }

        protected override void OnStylesApplied(string property, string value)
            => Log("OnStylesApplied", $"<{TagName}> стиль: {property}={value}");

        protected override void OnClassListApplied(string cssClass)
            => Log("OnClassListApplied", $"<{TagName}> клас: .{cssClass}");

        private void Log(string hook, string message)
            => Console.WriteLine($"{_label} {hook}: {message}");

        private static string Truncate(string s, int max = 20)
            => s.Length <= max ? s : s[..max] + "...";
    }
}
