
namespace Membership.Core;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipCoreServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter, Action<AppClientInfoOptions> appClientInfoOptionsSetter,
        Action<IDPClientInfoOptions> idpClientInfoOptionsSetter)
    {
        services.AddScoped<IRegisterInputPort, RegisterInteractor>();
        services.AddScoped<ILoginInputPort, LoginInteractor>();
        services.AddScoped<ILogoutInputPort, LogoutInteractor>();
        services.AddScoped<IRefreshTokenInputPort, RefreshTokenInteractor>();

        services.AddScoped<ILoginOutputPort, LoginPresenter>();
        services.AddScoped<IRefreshTokenOutputPort, RefreshTokenPresenter>();

        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddOptions<JwtOptions>().Configure(jwtOptionsSetter);
        services.AddSingleton<IAccessTokenService, AccessTokenService>();


        services.AddMembershipInternalServices(jwtOptionsSetter, appClientInfoOptionsSetter, idpClientInfoOptionsSetter);

        return services;
    }
}
