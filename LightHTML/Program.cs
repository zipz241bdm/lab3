namespace LightHTML
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            static LightElementNode Cell(string tag, string text, params string[] classes) =>
                new LightElementNode(tag, DisplayType.Inline, ClosingType.WithClosingTag, classes)
                    .AddChild(new LightTextNode(text));

            var table = new LightElementNode("table", cssClasses: new[] { "data-table" });

            var thead = new LightElementNode("thead");
            var headerRow = new LightElementNode("tr");
            foreach (var h in new[] { "#", "Мова", "Парадигма", "Рік" })
                headerRow.AddChild(Cell("th", h, "header-cell"));
            thead.AddChild(headerRow);
            table.AddChild(thead);

            var tbody = new LightElementNode("tbody");
            var rows = new[]
            {
                new[] { "1", "C#",         "ООП / Функціональна", "2000" },
                new[] { "2", "Python",     "Мультипарадигмова",   "1991" },
                new[] { "3", "JavaScript", "Прототипна / ООП",    "1995" },
                new[] { "4", "Rust",       "Системна / ФП",       "2010" },
            };
            foreach (var r in rows)
            {
                var tr = new LightElementNode("tr");
                foreach (var cell in r)
                    tr.AddChild(Cell("td", cell));
                tbody.AddChild(tr);
            }
            table.AddChild(tbody);

            var ul = new LightElementNode("ul", cssClasses: new[] { "feature-list" });
            foreach (var item in new[] { "Типобезпека", "LINQ", "Асинхронність (async/await)", "Записи (records)" })
            {
                var li = new LightElementNode("li").AddChild(new LightTextNode(item));
                ul.AddChild(li);
            }

            var img = new LightElementNode("img", DisplayType.Inline, ClosingType.SelfClosing, new[] { "logo" });

            var title = new LightElementNode("h2").AddChild(new LightTextNode("Популярні мови програмування"));

            var subTitle = new LightElementNode("p").AddChild(new LightTextNode("Особливості мови C#:"));

            var div = new LightElementNode("div", cssClasses: new[] { "languages", "main-content" });
            div.AddChild(img)
                .AddChild(title)
                .AddChild(table)
                .AddChild(subTitle)
                .AddChild(ul);

            Console.WriteLine("LightHTML - OuterHTML демонстрація");
            Console.WriteLine(div.OuterHTML());

            Console.WriteLine("\nInnerHTML таблиці (тільки вміст)");
            Console.WriteLine(table.InnerHTML());
        }
    }
}
