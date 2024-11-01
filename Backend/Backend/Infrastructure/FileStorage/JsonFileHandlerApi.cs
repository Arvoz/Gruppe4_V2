using Backend.Core.Ports;
using System.Text.Json;

namespace Backend.Infrastructure.FileStorage
{
    public class JsonFileHandlerApi<T> : IJsonFileHandlerApi<T>
    {
        public async Task<T> ReadFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return default(T);
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }

        public async Task SaveToFileAsync(T item, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, item);
            }
        }
    }
}
