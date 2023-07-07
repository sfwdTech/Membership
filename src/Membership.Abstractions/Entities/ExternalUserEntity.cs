namespace Membership.Abstractions.Entities;
public class ExternalUserEntity
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    public string LogingProvider { get; }
    public string ProviderUserId { get; }

    public ExternalUserEntity(string firstName,
                              string lastName,
                              string email,
                              string logingProvider,
                              string providerUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        LogingProvider = logingProvider;
        ProviderUserId = providerUserId;
    }
}
