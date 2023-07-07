namespace Membership.Abstractions.Interfaces.Logout;
public interface ILogoutInputPort
{
    Task LogoutAsync(UserTokensDTO userTokens);
}
