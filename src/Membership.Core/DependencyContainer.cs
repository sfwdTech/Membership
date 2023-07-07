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

        services.AddMembershipInteractorsServices()
            .AddMembershipPresentersServices()
            .AddMembershipControllerServices()
            .AddMembershipInternalServices(jwtOptionsSetter);

        return services;
    }
}
