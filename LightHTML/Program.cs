using LightHTML.EventListener;

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
            
            var img = new LightImageNode(
                href: "assets/Csharp_Logo.png",
                alt: "Logo",
                cssClasses: new[] { "logo" })
                .WithSize("400", "400");

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

            Console.WriteLine("\nКілька слухачів - <table>");

            var logger  = new ConsoleLogger("TableLogger");
            var counter = new ClickCounter();

            table.AddEventListener("click", logger);
            table.AddEventListener("click", counter);
            table.AddEventListener("click", counter);

            table.DispatchEvent("click");
            table.DispatchEvent("click");

            Console.WriteLine("\nРізні типи подій - <img> mouseover / mouseout");

            var highlighter = new HoverHighlighter();
            img.AddEventListener("mouseover", highlighter);
            img.AddEventListener("mouseout", highlighter);

            img.DispatchEvent("mouseover");
            img.DispatchEvent("mouseout");

            Console.WriteLine("\nОдин логер на <h2> і <ul> (click)");

            var sharedLogger = new ConsoleLogger("SharedLogger");
            title.AddEventListener("click", sharedLogger);
            ul.AddEventListener("click", sharedLogger);

            title.DispatchEvent("click");
            ul.DispatchEvent("click");

            Console.WriteLine("\nOnceListener на <div> (mouseover) - лише 1 раз");

            var onceLog = new OnceListener("mouseover", new ConsoleLogger("OnceLog"));
            div.AddEventListener("mouseover", onceLog);

            div.DispatchEvent("mouseover");
            div.DispatchEvent("mouseover");

            Console.WriteLine("\nRemoveEventListener - видалено logger, лишився counter");

            table.RemoveEventListener("click", logger);
            table.DispatchEvent("click");

            Console.WriteLine("\nПодія без підписників - <p> keydown");
            subTitle.DispatchEvent("keydown");

            Console.WriteLine("\nЗавантаження фото за допомогою FileImageLoadStrategy");
            var fileImg = new LightImageNode(
                href: "assets/Csharp_Logo.png",
                alt: "Логотип",
                cssClasses: new[] { "logo", "header-img" })
                .WithSize("120", "120");
            var fileImgHtml = fileImg.OuterHTML();
            Console.WriteLine($"{fileImgHtml[..40]} ... {fileImgHtml[^80..]}");

            Console.WriteLine("\nЗавантаження фото за допомогою NetworkImageLoadStrategy");
            var netImg = new LightImageNode(
                href: "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png",
                alt: "Логотип")
                .WithSize("200", "200")
                .AddClass("logo");
            var netImgHtml = netImg.OuterHTML();
            Console.WriteLine($"{netImgHtml[..40]} ... {netImgHtml[^80..]}");
        }
    }
}
