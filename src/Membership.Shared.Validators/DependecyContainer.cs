namespace Membership.Shared.Validators;
public static class DependecyContainer
{
    public static IServiceCollection AddMembershipValidatorsServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserDTO>, UserDTOValidator>();
        services.AddScoped<IValidator<UserCredentialsDTO>, UserCredentialsDTOValidator>();

        return services;
    }
}
