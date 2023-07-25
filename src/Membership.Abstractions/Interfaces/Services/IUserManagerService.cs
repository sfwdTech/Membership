using Membership.Shared.ValueObjects;

namespace Membership.Abstractions.Interfaces.Services;
public interface IUserManagerService
{
    Task<IEnumerable<MembershipError>> RegisterUserAsync(UserDTO user);
    Task<IEnumerable<MembershipError>> RegisterExternalUserAsync(ExternalUserEntity externalUserEntity);
    Task<UserEntity> GetUserByCredentialsAsync(UserCredentialsDTO userCredentials);
    Task<UserEntity> GetUserByExternalCredentialsAsync(ExternalUserCredentials externalUserCredentials);

    async Task ThrowIfUnableToRegisterUserAsync(UserDTO user)
    {
        var errors = await RegisterUserAsync(user);
        if (errors != null && errors.Any())
        {
            throw new RegisterUserException(errors);
        }
    }

    async Task ThrowIfUnableToRegisterExternalUserAsync(ExternalUserEntity externalUserEntity)
    {
        var errors = await RegisterExternalUserAsync(externalUserEntity);
        if (errors != null && errors.Any())
        {
            throw new RegisterUserException(errors);
        }
    }

    async Task<UserEntity> ThrowIfUnableToGetUserByCredentialsAsync(UserCredentialsDTO userCredentials)
    {
        var user = await GetUserByCredentialsAsync(userCredentials);
        return user == default ? throw new LoginUserException() : user;
    }
}
