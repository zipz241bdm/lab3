namespace LightHTML.ImageLoad
{
    public static class ImageStrategyFactory
    {
        public static IImageLoadStrategy Resolve(string href)
        {
            if (href.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                href.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return new NetworkImageLoadStrategy();

            return new FileImageLoadStrategy();
        }
    }
}
