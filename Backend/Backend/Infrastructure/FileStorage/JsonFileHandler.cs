﻿using System.Text.Json;

namespace Backend.Infrastructure.FileStorage
{
    public class JsonFileHandler<T> : IJsonFileHandler<T>
    {
        private readonly string _filePath;

        public JsonFileHandler(string filePath)
        {
            _filePath = filePath;
        }


        public async Task<List<T>> ReadFromFileAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                var result = await JsonSerializer.DeserializeAsync<List<T>>(stream);
                return result ?? new List<T>();
            }
        }

        public async Task SaveToFileAsync(List<T> item)
        {
            using (var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, item);
            }
        }


    }
}