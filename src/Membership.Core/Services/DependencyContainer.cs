using Membership.Shared.OAuth;
namespace Membership.Core.Services;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipInternalServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter, Action<AppClientInfoOptions> appClientInfoOptionsSetter,
        Action<IDPClientInfoOptions> idpClientInfoOptionsSetter)
    {

        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddOptions<JwtOptions>().Configure(jwtOptionsSetter);
        services.AddSingleton<IAccessTokenService, AccessTokenService>();

        // Para IAppClientService
        services.AddOptions<AppClientInfoOptions>().Configure(appClientInfoOptionsSetter);
        services.AddSingleton<IAppClientService, AppClientService>();

        // Para IIDPClientService
        services.AddHttpClient();
        services.AddOptions<IDPClientInfoOptions>().Configure(idpClientInfoOptionsSetter);
        services.AddSingleton<IIDPService, IDPService>();

        // Para IOAuthStateService
        services.TryAddSingleton<IMemoryCache, MemoryCache>();
        services.AddSingleton<IOAuthStateService, OAuthStateService>();

        services.AddOAuthService();

        return services;
    }
}
