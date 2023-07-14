namespace Membership.RefreshTokenService.MemoryCache;
public static class DependencyContainer
{
    public static IServiceCollection AddRefreshTokenMemoryCacheServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IMemoryCache, Microsoft.Extensions.Caching.Memory.MemoryCache>();
        services.AddSingleton<IRefreshTokenService, RefreshTokenService>();
        return services;
    }
}
