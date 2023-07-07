namespace Membership.Shared.MessageLocalizer;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipMessageLocalizer(this IServiceCollection services)
    {
        services.TryAddScoped<IMembershipMessageLocalizer, MembershipMessageLocalizer>();

        return services;
    }
}
