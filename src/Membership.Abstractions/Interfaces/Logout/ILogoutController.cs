namespace Membership.Abstractions.Interfaces.Logout;
public interface ILogoutController
{
    Task LogoutAsycn(UserTokensDTO userTokens);
}
