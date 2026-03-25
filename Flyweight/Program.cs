using System.Text;
using LightHTML;

namespace Flyweight
{
    static class Scaling
    {
        public static string Scale(this long num)
        {
            const int factor = 1024;
            const string format = "0.00";
            double scaledNum = num;
            int size = 1;
            while (scaledNum > factor)
            {
                size++;
                scaledNum /= factor;
            }
            var str = new StringBuilder(scaledNum.ToString(format));
            str.Append(" " + size switch
            {
                1 => "B",
                2 => "KB",
                3 => "MB",
                4 => "GB",
                5 => "TB",
                _ => throw new ArgumentException("Invalid size ", nameof(num))
            });
            return str.ToString();
        }
    }
    
    class Program
    {
        static string[] LoadBookLines(string path)
        {
            var lines  = new List<string>();
            bool inside = false;
            foreach (string line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (line.StartsWith("*** START OF THE PROJECT GUTENBERG")) { 
                    inside = true;
                    continue;
                }
                if (line.StartsWith("*** END OF THE PROJECT GUTENBERG"))
                    break;
                if (!inside || string.IsNullOrWhiteSpace(line))
                    continue;
                lines.Add(line.TrimEnd());
            }
            return lines.ToArray();
        }

        static string Classify(string line, bool isFirst)
        {
            if (isFirst) return "h1";
            if (line.Length < 20) return "h2";
            if (line.Length > 0 && line[0] == ' ')
                return "blockquote";
            return "p";
        }

        static LightElementNode BuildTree(string[] lines)
        {
            var body  = new LightElementNode("body");
            bool first = true;
            foreach (string line in lines)
            {
                var tag = Classify(line, first);
                first = false;
                body.AddChild(
                    new LightElementNode(tag)
                        .AddChild(new LightTextNode(line.Trim())));
            }
            return body;
        }

        static LightElementNodeFw BuildTree_Flyweight(string[] lines)
        {
            var body  = new LightElementNodeFw("body");
            bool first = true;
            foreach (string line in lines)
            {
                var tag = Classify(line, first);
                first = false;
                body.AddChild(
                    new LightElementNodeFw(tag)
                        .AddChild(new LightTextNode(line.Trim())));
            }
            return body;
        }

        static void Main()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
                Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            const string bookPath = "Romeo and Juliet.txt";

            string[] bookLines = LoadBookLines(bookPath);
            
            var warmUpTree1 = BuildTree(bookLines);
            _ = warmUpTree1.OuterHTML();
            warmUpTree1 = null;

            var warmUpTree2 = BuildTree_Flyweight(bookLines);
            _ = warmUpTree2.OuterHTML();
            warmUpTree2 = null;
            ElementFlyweightFactory.Reset();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);


            Console.WriteLine("Побудова дерева без Легковаговика: ");

            Console.WriteLine("Пам'ять перед створенням дерева:");
            long oldMemory = GC.GetTotalMemory(true);
            Console.WriteLine($"Виділена пам'ять: {oldMemory.Scale()}");

            LightElementNode oldRoot = BuildTree(bookLines);

            Console.WriteLine("Пам'ять після створення дерева:");
            long newMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"Виділена пам'ять: {newMemory.Scale()}");

            long memoryDiff = newMemory - oldMemory;
            Console.WriteLine($"Різниця пам'яті (вага дерева): {memoryDiff.Scale()}");

            oldRoot = null!;
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();


            Console.WriteLine("\nПобудова дерева з Легковаговиком: ");

            Console.WriteLine("Пам'ять перед створенням дерева:");
            oldMemory = GC.GetTotalMemory(true);
            Console.WriteLine($"Виділена пам'ять: {oldMemory.Scale()}");

            LightElementNodeFw newRoot = BuildTree_Flyweight(bookLines);

            Console.WriteLine("Пам'ять після створення дерева:");
            newMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"Виділена пам'ять: {newMemory.Scale()}");

            long memoryDiffFw = newMemory - oldMemory;
            Console.WriteLine($"Різниця пам'яті (вага дерева): {memoryDiffFw.Scale()}");
            Console.WriteLine($"Покращено на {(memoryDiff - memoryDiffFw) * 100.0 / memoryDiff:F2}%");

            Console.WriteLine();
            ElementFlyweightFactory.PrintCache();
        }
    }
}