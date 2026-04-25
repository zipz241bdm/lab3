namespace LightHTML.Visitor
{
    public class AccessibilityCheckerVisitor : ILightNodeVisitor
    {
        private List<string> _errors = new();

        public bool IsValid => _errors.Count == 0;
        public IEnumerable<string> Errors => _errors;

        public void Visit(LightElementNode node) { }

        public void Visit(LightTextNode node) { }

        public void Visit(LightImageNode node)
        {
            if (string.IsNullOrWhiteSpace(node.AltText))
            {
                string href = node.Href.Length <= 63
                    ? node.Href
                    : $"{node.Href[..30]}...{node.Href[^30..]}";
                _errors.Add(
                    $"Попередження доступності: зображення з джерелом '{href}' не має тексту alt.");
            }
        }

        public void PrintResults()
        {
            if (IsValid)
            {
                Console.WriteLine("Перевірка доступності пройдена успішно!");
                return;
            }

            Console.WriteLine("Виявлені проблеми з доступністю:");
            foreach (var error in _errors)
            {
                Console.WriteLine($"- {error}");
            }
        }
    }
}