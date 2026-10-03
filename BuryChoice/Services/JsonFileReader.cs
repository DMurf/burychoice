using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace BuryChoice.Services
{
    public class JsonFileReader : IJsonFileReader
    {
        private readonly IHostEnvironment _env;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip
        };

        public JsonFileReader(IHostEnvironment env)
        {
            _env = env;
        }

        public async Task<T?> ReadJsonFileAsync<T>(string relativePath, bool fromWebRoot = false)
        {
            var root = fromWebRoot
                ? Path.Combine(_env.ContentRootPath, "wwwroot")
                : _env.ContentRootPath;

            var fullPath = Path.GetFullPath(Path.Combine(root, relativePath));

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"JSON file not found: {fullPath}");

            var json = await File.ReadAllTextAsync(fullPath);
            return JsonSerializer.Deserialize<T>(json, _options);
        }
    }
}
