namespace Application.Data;

public interface ICacheService
{
    Task<T?> GetDataAsync<T>(string key, CancellationToken token = default);
    
    Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime,  CancellationToken token = default);

    Task<bool> RemoveDataAsync(string key, CancellationToken token = default);
}