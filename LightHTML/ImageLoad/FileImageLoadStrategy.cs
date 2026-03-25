namespace LightHTML.ImageLoad
{
    public class FileImageLoadStrategy : IImageLoadStrategy
    {
        public string Load(string href)
        {
            if (!File.Exists(href))
                throw new FileNotFoundException($"Файл не знайдено: {href}");

            string ext = Path.GetExtension(href).TrimStart('.').ToLowerInvariant();
            string mime = ext switch
            {
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                "gif" => "image/gif",
                "webp" => "image/webp",
                "svg" => "image/svg+xml",
                _ => "application/octet-stream"
            };

            byte[] bytes = File.ReadAllBytes(href);
            string base64 = Convert.ToBase64String(bytes);
            string dataUri = $"data:{mime};base64,{base64}";

            Console.WriteLine($"FileStrategy - Завантажено з файлу: {href} ({bytes.Length} байт, {mime})");
            return dataUri;
        }
    }
}
