using LightHTML;

namespace Flyweight
{
    public static class ElementFlyweightFactory
    {
        private static readonly Dictionary<(string tag, DisplayType disp, ClosingType clos), ElementFlyweight>
            _cache = new();

        public static ElementFlyweight Get(
            string tagName, DisplayType display, ClosingType closing)
        {
            var key = (tagName, display, closing);

            if (!_cache.TryGetValue(key, out var fw))
            {
                fw = new ElementFlyweight(tagName, display, closing);
                _cache[key] = fw;
            }
            return fw;
        }

        public static int CacheSize => _cache.Count;

        public static void PrintCache()
        {
            Console.WriteLine($"Кеш легковаговиків ({_cache.Count} об'єктів):");
            foreach (var kv in _cache)
                Console.WriteLine(kv.Value);
        }

        public static void Reset() => _cache.Clear();
    }
}