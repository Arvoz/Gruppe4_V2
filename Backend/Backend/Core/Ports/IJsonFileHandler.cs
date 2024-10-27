public interface IJsonFileHandler<T>
{
    Task<List<T>> ReadFromFileAsync();
    Task SaveToFileAsync(List<T> items);
}