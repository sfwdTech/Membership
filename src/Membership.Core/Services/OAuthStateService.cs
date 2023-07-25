namespace Membership.Core.Services;
internal class OAuthStateService : IOAuthStateService
{
    readonly IMemoryCache _cache;

    public OAuthStateService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T> GetAsync<T>(string key)
    {
        _cache.TryGetValue(key, out T value);
        return Task.FromResult<T>(value);
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value)
    {
        _cache.Set(key, value, DateTime.Now.AddMinutes(5));
        return Task.CompletedTask;
    }
}
