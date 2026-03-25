namespace Proxy
{
    public class SmartTextChecker : ITextReader
    {
        private readonly ITextReader _inner;

        public SmartTextChecker(ITextReader inner)
        {
            _inner = inner;
        }

        public char[][] ReadFile(string filePath)
        {
            Console.WriteLine($"[LOG] Відкриття файлу: {filePath}");

            char[][] result;
            try
            {
                result = _inner.ReadFile(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG] Помилка під час читання файлу: {ex.Message}");
                throw;
            }

            Console.WriteLine($"[LOG] Файл успішно прочитано.");
            Console.WriteLine($"[LOG] Файл закрито.");

            int totalChars = result.Sum(line => line.Length);
            Console.WriteLine($"[STATS] Всього рядків: {result.Length}");
            Console.WriteLine($"[STATS] Всього символів: {totalChars}");

            return result;
        }
    }
}