namespace Backend.Core.Ports
{
    public interface IJsonFileHandlerApi<T>
    {
        Task<T> ReadFromFileAsync(string filePath);
        Task SaveToFileAsync(T item, string filePath);

    }
}
