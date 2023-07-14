using Membership.Core.Controllers;
using Membership.Core.Interactors;
using Membership.Core.Presenters;
using Membership.Core.Services;

namespace Membership.Core;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipCoreServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter)
    {

        //services
        //    .AddMembershipControllerServices()
        //    .AddMembershipInteractorsServices()
        //    .AddMembershipPresentersServices()
        //    .AddMembershipInternalServices(jwtOptionsSetter);

        services.AddScoped<IRegisterController, RegisterController>();
        services.AddScoped<ILoginController, LoginController>();
        services.AddScoped<ILogoutController, LogoutController>();
        services.AddScoped<IRefreshTokenController, RefreshTokenController>();

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

        return services;
    }
}
