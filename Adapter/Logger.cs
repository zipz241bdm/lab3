namespace Adapter
{
    class Logger : ILogger
    {
        public void Log(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public void Error(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public void Warn(string str)
        {
            Console.WriteLine($"\x1B[38;5;214m{str}\x1B[0m");
        }
    }
}