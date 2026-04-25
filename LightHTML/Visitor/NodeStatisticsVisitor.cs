namespace LightHTML.Visitor
{
    public class NodeStatisticsVisitor : ILightNodeVisitor
    {
        public int TotalNodes { get; private set; }
        public int ElementNodes { get; private set; }
        public int TextNodes { get; private set; }
        public int ImageNodes { get; private set; }
        
        public Dictionary<string, int> TagCounts { get; } = new();

        public void Visit(LightElementNode node)
        {
            TotalNodes++;
            ElementNodes++;
            
            string tagName = node.TagName.ToLower();
            if (TagCounts.TryGetValue(tagName, out int value))
                TagCounts[tagName] = value + 1;
            else
                TagCounts[tagName] = 1;
        }

        public void Visit(LightTextNode node)
        {
            TotalNodes++;
            TextNodes++;
        }

        public void Visit(LightImageNode node)
        {
            TotalNodes++;
            ImageNodes++;
            ElementNodes++;
        }

        public void PrintReport()
        {
            Console.WriteLine("Статистика DOM-дерева:");
            Console.WriteLine($"Усього вузлів: {TotalNodes}");
            Console.WriteLine($"Елементів: {ElementNodes}");
            Console.WriteLine($"Текстових вузлів: {TextNodes}");
            Console.WriteLine($"Зображень: {ImageNodes}");
            Console.WriteLine("Деталі по тегах:");
            foreach (var tag in TagCounts)
            {
                Console.WriteLine($" - <{tag.Key}>: {tag.Value}");
            }
        }
    }
}