namespace Membership.Abstractions.Interfaces.Login;
public interface ILoginController
{
    Task<UserTokensDTO> LogingAsync(UserCredentialsDTO userCredentials);
}
