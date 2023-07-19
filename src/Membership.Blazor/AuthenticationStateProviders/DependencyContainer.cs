namespace Membership.Blazor.AuthenticationStateProviders;
public static class DependencyContainer
{
    public static IServiceCollection AddMembershipAuthenticationProvider(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationStateProvider, JWTAuthencticationStateProvider>();

        services.AddScoped(provider => (AuthenticationStateProvider)
            provider.GetRequiredService<IAuthenticationStateProvider>());

        return services;
    }
}
