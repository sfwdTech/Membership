namespace Membership.Core.Services;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipInternalServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter)
    {

        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddOptions<JwtOptions>().Configure(jwtOptionsSetter);
        services.AddSingleton<IAccessTokenService, AccessTokenService>();

        return services;
    }
}
