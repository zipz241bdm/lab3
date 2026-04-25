namespace LightHTML.Lifecycle
{
    public class LoggedTextNode(string text, string label = "TextNode") : LightTextNode(text)
    {
        private readonly string _label = label;

        protected override void OnCreated()
            => Console.WriteLine($"#text {_label} OnCreated: текстовий вузол \"{Truncate(Text)}\" створено");

        protected override void OnTextRendered(string text)
            => Console.WriteLine($"#text {_label} OnTextRendered: \"{Truncate(text)}\"");

        private static string Truncate(string s, int max = 30)
            => s.Length <= max ? s : s[..max] + "...";
    }
}
