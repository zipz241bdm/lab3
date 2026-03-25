namespace Adapter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Консольний Logger: ");

            ILogger consoleLogger = new Logger();
            consoleLogger.Log("Програму запущено успішно");
            consoleLogger.Warn("Файл конфігурації не знайдено, використовуються значення за замовчуванням");
            consoleLogger.Error("Помилка підключення до бази даних\n");

            Console.WriteLine("Файловий Logger (Adapter): ");

            string logFile = "app.log";

            if (File.Exists(logFile))
                File.Delete(logFile);

            ILogger fileLogger = new FileLoggerAdapter(logFile);
            fileLogger.Log("Програму запущено успішно");
            fileLogger.Warn("Файл конфігурації не знайдено, використовуються значення за замовчуванням");
            fileLogger.Error("Помилка підключення до бази даних");

            Console.WriteLine($"Записи збережено у файл: {Path.GetFullPath(logFile)}");
            Console.WriteLine("\nВміст файлу:");
            Console.WriteLine(File.ReadAllText(logFile));
        }
    }
}