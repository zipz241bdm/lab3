namespace LightHTML.ImageLoad
{
    public class NetworkImageLoadStrategy : IImageLoadStrategy
    {
        private static readonly HttpClient _http = new();

        static NetworkImageLoadStrategy()
        {
            _http.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/124.0.0.0 Safari/537.36");
            _http.DefaultRequestHeaders.Add("Accept",
                "image/avif,image/webp,image/apng,image/*,*/*;q=0.8");
            _http.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        }
 
        public string Load(string href)
        {
            if (!Uri.TryCreate(href, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                throw new ArgumentException($"Недійсний URL: {href}");

            using var response = _http.GetAsync(href).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            string mime = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
            byte[] bytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            string base64 = Convert.ToBase64String(bytes);
            string dataUri = $"data:{mime};base64,{base64}";

            Console.WriteLine($"NetworkStrategy - Завантажено з мережі: {href} ({bytes.Length} байт, {mime})");
            return dataUri;
        }
    }
}

