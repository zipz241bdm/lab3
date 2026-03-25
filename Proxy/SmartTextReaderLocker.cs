using System.Text.RegularExpressions;

namespace Proxy
{
    public class SmartTextReaderLocker : ITextReader
    {
        private readonly ITextReader _inner;
        private readonly Regex _restrictedPattern;

        public SmartTextReaderLocker(ITextReader inner, string restrictedPattern)
        {
            _inner = inner;
            _restrictedPattern = new Regex(restrictedPattern, RegexOptions.IgnoreCase);
        }

        public char[][] ReadFile(string filePath)
        {
            if (_restrictedPattern.IsMatch(filePath))
            {
                Console.WriteLine("Доступ заборонено!");
                return [];
            }

            return _inner.ReadFile(filePath);
        }
    }
}