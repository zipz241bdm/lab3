namespace Proxy
{
    class Program
    {
        static void PrintResult(char[][] data)
        {
            if (data.Length == 0) { 
                Console.WriteLine("(немає даних)"); 
                return; 
            }
            for (int i = 0; i < data.Length; i++)
                Console.WriteLine($"Рядок {i + 1}: {new string(data[i])}");
        }

        static void Main()
        {
            string allowedPath = "allowed.txt";
            File.WriteAllText(allowedPath, "Привіт, світе!\nДругий рядок.\nТретій рядок.");
            string secretPath = "secret_data.txt";
            File.WriteAllText(secretPath, "Цілком секретний контент.\nНе читати!");

            Console.WriteLine("SmartTextReader: ");

            ITextReader plainReader = new SmartTextReader();
            char[][] plainResult = plainReader.ReadFile(allowedPath);
            PrintResult(plainResult);

            Console.WriteLine("\nSmartTextChecker (проксі для логування): ");

            ITextReader checkerReader = new SmartTextChecker(new SmartTextReader());
            char[][] checkedResult = checkerReader.ReadFile(allowedPath);
            Console.WriteLine("Вміст: ");
            PrintResult(checkedResult);

            Console.WriteLine("\nSmartTextReaderLocker (дозволений файл): ");

            ITextReader locker = new SmartTextReaderLocker(
                new SmartTextChecker(new SmartTextReader()),
                @"secret"
            );

            char[][] allowedResult = locker.ReadFile(allowedPath);
            Console.WriteLine("Вміст: ");
            PrintResult(allowedResult);

            Console.WriteLine("\nSmartTextReaderLocker (недозволений файл): ");

            char[][] deniedResult = locker.ReadFile(secretPath);
            Console.WriteLine("Вміст: ");
            PrintResult(deniedResult);

            File.Delete(allowedPath);
            File.Delete(secretPath);
        }
    }
}