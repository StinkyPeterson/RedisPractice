using System.Text.Json;
using Application.Contracts.Contracts;
using StackExchange.Redis;

namespace Infrastructure.Redis.Cache;

public class RedisService : ICacheService
{
    private readonly IDatabase _redisDb;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
    }
    
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        await _redisDb.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _redisDb.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
    }

    public async Task RemoveAsync(string key)
    {
        await _redisDb.KeyDeleteAsync(key);
    }
}