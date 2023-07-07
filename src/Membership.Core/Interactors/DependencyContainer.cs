using Microsoft.Extensions.DependencyInjection;

namespace Membership.Core.Interactors;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipInteractorsServices(this IServiceCollection services)
    {

        services.AddScoped<IRegisterInputPort, RegisterInteractor>();
        services.AddScoped<ILoginInputPort, LoginInteractor>();
        services.AddScoped<ILogoutInputPort, LogoutInteractor>();
        services.AddScoped<IRefreshTokenInputPort, RefreshTokenInteractor>();

        return services;
    }
}
