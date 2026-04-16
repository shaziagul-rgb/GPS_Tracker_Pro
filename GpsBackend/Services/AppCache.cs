using Microsoft.Extensions.Caching.Memory;

namespace GpsBackend.Services;

public class AppCache : IAppCache
{
    private readonly IMemoryCache _cache;

    public AppCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        _cache.TryGetValue(key, out T? value);
        return value;
    }
    public void Set<T>(string key, T value, TimeSpan ttl)
    {
        _cache.Set(key, value, ttl);
    }
}