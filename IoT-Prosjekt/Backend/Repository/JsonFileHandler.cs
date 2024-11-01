using Backend.Ports;
using System.Text.Json;

namespace Backend.Repository
{
    public class JsonFileHandler<T> : IJsonFileHandler<T>
    {
        public async Task<List<T>> ReadFromFileList(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var result = await JsonSerializer.DeserializeAsync<List<T>>(stream);
                return result ?? new List<T>();
            }
        }

        public async Task SaveToFileList(List<T> list, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, list);
            }
        }

        public async Task SaveToFile(T obj, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, obj);
            }
        }

        public async Task<T> ReadFromFile(string filePath)
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
    }
}
