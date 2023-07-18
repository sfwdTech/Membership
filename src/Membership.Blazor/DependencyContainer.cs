using Membership.Blazor.Repositories;

namespace Membership.Blazor;
public static class DependencyContainer
{
    internal static IHttpClientBuilder AddExceptionDelegatingHandler(this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<ExceptionDelegatingHandler>();
        builder.AddHttpMessageHandler<ExceptionDelegatingHandler>();

        return builder;
    }

    public static IHttpClientBuilder AddMembershipBearerTokenHandler(this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<BearerTokenHandler>();
        builder.AddHttpMessageHandler<BearerTokenHandler>();

        return builder;
    }

    public static IServiceCollection AddMembershipGateways(this IServiceCollection services,
        Action<UserEndpointsOptions> userEndpointsOptionsSetter)
    {
        services.AddOptions<UserEndpointsOptions>()
            .Configure(userEndpointsOptionsSetter);

        services.AddHttpClient<IUserWebApiGateway, UserWebApiGateway>()
            .AddExceptionDelegatingHandler();

        return services;
    }

    public static IServiceCollection AddMembershipRepository(this IServiceCollection services)
    {
        services.AddScoped<ITokensRepository, TokensRepository>();

        return services;
    }
}
