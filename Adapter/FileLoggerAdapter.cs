namespace Adapter
{
    class FileLoggerAdapter : ILogger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(string filePath)
        {
            _fileWriter = new FileWriter(filePath);
        }

        public void Log(string str)
        {
            _fileWriter.WriteLine($"[Log] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {str}");
        }

        public void Error(string str)
        {
            _fileWriter.WriteLine($"[Error] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {str}");
        }

        public void Warn(string str)
        {
            _fileWriter.WriteLine($"[Warn] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {str}");
        }
    }
}