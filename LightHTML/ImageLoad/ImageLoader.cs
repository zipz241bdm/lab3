namespace LightHTML.ImageLoad
{
    public class ImageLoader
    {
        IImageLoadStrategy _strategy = new FileImageLoadStrategy();

        public void SetStrategy(IImageLoadStrategy strategy)
        {
            _strategy = strategy;
        }

        public string Load(string href)
        {
            return _strategy.Load(href);
        }

        public string AutoLoad(string href)
        {
            _strategy = ImageStrategyFactory.Resolve(href);
            return _strategy.Load(href);
        }
    }
}
