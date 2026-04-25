using LightHTML.EventListener;
using LightHTML.Lifecycle;
using LightHTML.Iterator;

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


            Console.WriteLine("\n\nШаблонний метод - хуки життєвого циклу");

            Console.WriteLine("\nСтворення LoggedElementNode <section>:");
            var section = new LoggedElementNode("section", label: "section");

            Console.WriteLine("\nAddClass() - OnClassListApplied:");
            section.AddClass("container");
            section.AddClass("main");

            Console.WriteLine("\nAddStyle() - OnStylesApplied:");
            section.AddStyle("color", "#333");
            section.AddStyle("font-size", "16px");

            Console.WriteLine("\nСтворення LoggedTextNode:");
            var loggedText = new LoggedTextNode("Привіт, LightHTML!", label: "h1-text");

            Console.WriteLine("\nAddChild() - OnInserted:");
            var h1 = new LoggedElementNode("h1", label: "h1");
            h1.AddChild(loggedText);
            section.AddChild(h1);

            Console.WriteLine("\nRemoveChild() - OnRemoved:");
            section.RemoveChild(h1);

            Console.WriteLine("\nOnTextRendered під час рендерингу (OuterHTML):");
            var paraText = new LoggedTextNode("Хуки життєвого циклу - потужний інструмент.", "p-text");
            var p = new LightElementNode("p").AddChild(paraText);
            _ = p.OuterHTML();

            
            Console.WriteLine("\n\nІтератор - Обхід дерева DFS і BFS");

            static LightElementNode El(string tag, params LightNode[] children)
            {
                var el = new LightElementNode(tag);
                foreach (var c in children) el.AddChild(c);
                return el;
            }
            static LightTextNode Tx(string text) => new LightTextNode(text);

            var root = El("div",
                El("header",
                    El("h1", Tx("Заголовок"))),
                El("main",
                    El("p", Tx("Перший абзац")),
                    El("ul",
                        El("li", Tx("Пункт A")),
                        El("li", Tx("Пункт B")))),
                El("footer",
                    El("p", Tx("Підвал")))
            );
            root.AddClass("root");

            Console.WriteLine("\nСтруктура документа (OuterHTML):");
            Console.WriteLine(root.OuterHTML());

            Console.WriteLine("\nОбхід в глибину (DFS, pre-order):");
            root.Traverse(TraversalType.DepthFirst, (node, i) =>
            {
                var label = node switch
                {
                    LightElementNode el => $"<{el.TagName}>",
                    LightTextNode tx => $"\"{tx.Text}\"",
                    _ => node.GetType().Name
                };
                Console.WriteLine($"  {i,2}. {label}");
            });

            Console.WriteLine("\nОбхід в ширину (BFS, level-order):");
            root.Traverse(TraversalType.BreadthFirst, (node, i) =>
            {
                var label = node switch
                {
                    LightElementNode el => $"<{el.TagName}>",
                    LightTextNode tx => $"\"{tx.Text}\"",
                    _ => node.GetType().Name
                };
                Console.WriteLine($"  {i,2}. {label}");
            });

            Console.WriteLine("\nРучний ітератор DFS: збираємо лише елементи (без текстових):");
            var dfs = root.AsEnumerable(TraversalType.DepthFirst).GetEnumerator();
            int count = 0;
            while (dfs.MoveNext())
            {
                var node = dfs.Current;
                if (node is LightElementNode el)
                {
                    var classes = el.CssClasses.Count > 0
                        ? $" {string.Join(", ", el.CssClasses.Select(c => "." + c))}" : "";
                    Console.WriteLine($"  <{el.TagName}>{classes}  ({el.ChildrenCount} дітей)");
                    count++;
                }
            }
            Console.WriteLine($"  Всього елементів: {count}");

            Console.WriteLine("\nReset() і повторний обхід BFS (тільки перші 4 вузли):");
            var bfs = root.AsEnumerable(TraversalType.BreadthFirst).GetEnumerator();
            for (int i = 0; i < 4 && bfs.MoveNext(); i++)
            {
                var node = bfs.Current;
                var label = node is LightElementNode e ? $"<{e.TagName}>" : $"\"{((LightTextNode)node).Text}\"";
                Console.WriteLine($"  {i + 1}. {label}");
            }
            Console.WriteLine("  ... скидаємо ітератор ...");
            bfs.Reset();
            bfs.MoveNext();
            Console.WriteLine($"  Після Reset першим знову є: <{((LightElementNode)bfs.Current).TagName}>");


            Console.WriteLine("\n\nПатерн Команда - Undo / Redo DOM-редактор");

            var history = new Command.CommandHistory();
            var article = new LightElementNode("article");
            var heading = new LightElementNode("h1").AddChild(new LightTextNode("LightHTML Editor"));
            var intro = new LightElementNode("p").AddChild(new LightTextNode("Вступний абзац."));
            var note = new LightElementNode("p").AddChild(new LightTextNode("Примітка."));

            Console.WriteLine("\nВиконуємо три команди");
            history.Execute(new Command.AddChildCommand(article, heading));
            history.Execute(new Command.AddChildCommand(article, intro));
            history.Execute(new Command.AddClassCommand(article, "blog-post"));

            Console.WriteLine("\nHTML після трьох команд:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nUndo x2 (скасуємо AddClass і другий AddChild)");
            history.Undo();
            history.Undo();

            Console.WriteLine("\nHTML після двох Undo:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nRedo x1 (повертаємо другий AddChild)");
            history.Redo();

            Console.WriteLine("\nHTML після Redo:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nSetStyle (нова команда - очищує Redo-стек)");
            history.Execute(new Command.SetStyleCommand(article, "color", "#222"));
            history.Execute(new Command.SetStyleCommand(article, "font-size", "18px"));

            Console.WriteLine("\nRedo вже не можливий (новий Execute скинув стек):");
            history.Redo();

            Console.WriteLine("\nHTML зі стилями:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nUndo SetStyle - відновлення попереднього значення");
            history.Execute(new Command.SetStyleCommand(article, "color", "red"));
            Console.WriteLine($"  color = {article.Styles["color"]}");
            history.Undo();
            Console.WriteLine($"  color після Undo = {article.Styles["color"]}");

            Console.WriteLine("\n6. MacroCommand - кілька операцій як одна");
            var macro = new Command.MacroCommand("Додати примітку зі стилем", new LightHTML.Command.ILightCommand[]
            {
                new Command.AddChildCommand(article, note),
                new Command.AddClassCommand(article, "has-note"),
                new Command.SetStyleCommand(article, "border", "1px solid #ccc"),
            });

            history.Execute(macro);

            Console.WriteLine("\nHTML після MacroCommand:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nUndo всього макросу одним кроком:");
            history.Undo();

            Console.WriteLine("\nHTML після Undo макросу:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nRemoveChildCommand зі збереженням позиції");
            history.Execute(new Command.AddChildCommand(article, note));

            Console.WriteLine("\nПеред RemoveChild:");
            Console.WriteLine(article.OuterHTML());

            history.Execute(new Command.RemoveChildCommand(article, heading));
            Console.WriteLine("\nПісля RemoveChild <h1>:");
            Console.WriteLine(article.OuterHTML());

            history.Undo();
            Console.WriteLine("\nПісля Undo - <h1> повернувся на свою позицію:");
            Console.WriteLine(article.OuterHTML());

            Console.WriteLine("\nФінальний стан стеків");
            history.PrintState();
        }
    }
}
