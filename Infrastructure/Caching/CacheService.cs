using System.Text.Json;
using Application.Data;
using StackExchange.Redis;

namespace Infrastructure.Caching;

internal class CacheService: ICacheService
{
    private IDatabase _cacheDb;

    public CacheService(IConnectionMultiplexer redis)
    {
        _cacheDb = redis.GetDatabase();
    }

    public Task<T?> GetDataAsync<T>(string key, CancellationToken token = default)
    {
        var value = _cacheDb.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return Task.FromResult(JsonSerializer.Deserialize<T>(value));
        }

        return Task.FromResult<T>(default);
    }

    public Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken token = default)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.UtcNow);

        var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);

        return Task.FromResult(isSet);
    }

    public Task<bool> RemoveDataAsync(string key, CancellationToken token = default)
    {
        var isExists = _cacheDb.KeyExists(key);

        if (isExists)
        {
            return Task.FromResult(_cacheDb.KeyDelete(key));
        }

        return Task.FromResult(false);
    }
}