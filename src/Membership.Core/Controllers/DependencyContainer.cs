namespace Membership.Core.Controllers;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipControllerServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterController, RegisterController>();
        services.AddScoped<ILoginController, LoginController>();
        services.AddScoped<ILogoutController, LogoutController>();
        services.AddScoped<IRefreshTokenController, RefreshTokenController>();

        return services;
    }
}
