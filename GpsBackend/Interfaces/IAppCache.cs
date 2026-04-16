public interface IAppCache
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan ttl);
}